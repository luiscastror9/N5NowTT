using Confluent.Kafka;
using Elastic.Apm.Api;
using N5NowTT.Application;
using N5NowTT.Domain;
using N5NowTT.Infrastructure;
using Nest;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using Newtonsoft.Json;
using N5NowTT.Infrastructure.Kafka;
using N5NowTT.Domain.DTO;
using Microsoft.Extensions.Hosting;

namespace N5NowTT.WebApi.Middleware
{
    public class ElasticMiddleware
    {
        private readonly RequestDelegate _next;
        private ElasticClient _elasticClient;

        public ElasticMiddleware(RequestDelegate next, ElasticClient elasticClient)
        {
            _next = next;
            _elasticClient= elasticClient;
        

    }
    public  Task Invoke(HttpContext httpContext)
        {
            //   

            var kafkaProducer = httpContext.RequestServices.GetRequiredService<KafkaProducer>();
            var _iUoW = httpContext.RequestServices.GetRequiredService<MiDbContext>();
            var request = httpContext.Request;
            var response = httpContext.Response;
            string stringResult;

            #region ELASTIC INDEX
            // Indexa en base a los metodos para saber que indexar si es necesario
            if (request.Method == HttpMethods.Get)
            {
                if (request.Path != "/GetPermissions")
                {
                    var permissions = _elasticClient.IndexMany<Permissions>(_iUoW.Permissions.ToList());

                }
                else { 
                var getMessage = new OperationDTO { Operation = OperationDTO.get };
                _ = kafkaProducer.ProduceAsync(getMessage);
                }
            }
            if (request.Method == HttpMethods.Post && response.ContentLength > 0)
            {
               
                using (StreamReader sr = new StreamReader(response.Body))
                {
                    stringResult = sr.ReadToEnd(); 
                }

                var result = JsonConvert.DeserializeObject<Permissions>(stringResult);
                var permissions = _elasticClient.IndexDocument<Permissions>(result);

                #region KAFKA MESSAGES 
                //Luis.castro: Revisa los metodos y en base a eso guarda el mensaje en Kafka 

                if (request.Path== "/ModifyPermission") {
                    var modifyMessage = new OperationDTO { Operation = OperationDTO.modify };
                    _ = kafkaProducer.ProduceAsync(modifyMessage);

                }
                else
                {
                    var requestMessage = new OperationDTO { Operation = OperationDTO.request };
                    _ = kafkaProducer.ProduceAsync(requestMessage);

                }
                #endregion

            }
            #endregion
            return _next(httpContext);
        }
    }
    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ElasticMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<ElasticMiddleware>();
        }
    }
}
