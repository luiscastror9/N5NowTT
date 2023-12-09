using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using N5NowTT.Application;
using N5NowTT.Infrastructure;
using Nest;
using System.Configuration;
using System.Data.Entity;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using N5NowTT.Infrastructure.Commands;
using N5NowTT.Domain;
using N5NowTT.Infrastructure.Queries;
using Microsoft.Extensions.Configuration;
using N5NowTT.WebApi.Middleware;
using Elastic.Apm.Api;
using System;
using Elasticsearch.Net;
using Confluent.Kafka;
using System.Reflection.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using N5NowTT.Infrastructure.Kafka;

internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        using IHost host = Host.CreateDefaultBuilder(args).Build();



        IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

        //Kafka Initialization
        var kafkaProducer = new KafkaProducer(config["Kafka:Uri"], config["Kafka:topic"]);
        builder.Services.AddSingleton(kafkaProducer);

        //DBContext Initialization
        builder.Services.AddDbContext<MiDbContext>(options =>
        {
            options.UseSqlServer(config["ConnectionStrings:Default"]);
            
        });


        //ElasticSearch Initialization
        var settings = new ConnectionSettings(new Uri(config["ElasticSearch:Uri"])).BasicAuthentication(config["ElasticSearch:user"], config["ElasticSearch:pwd"]).DefaultIndex(config["ElasticSearch:DefaultIndex"]);
       
        var client = new ElasticClient(settings);
        var createIndexResponse = client.Indices.Create(config["ElasticSearch:DefaultIndex"], c => c.Map<Permissions>(m => m.AutoMap<Permissions>()));
        builder.Services.AddSingleton(client);



        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        }

        builder.Services.AddTransient<IElasticClient, ElasticClient>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddHttpContextAccessor();


        builder.Services.AddControllers();


        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins(config["Front:Uri"]) // Reemplaza con la URL de tu aplicación React
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
        });
        var app = builder.Build();

        

        app.UseMyMiddleware();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();


        //initialize
        


        app.Run();
    }
}


