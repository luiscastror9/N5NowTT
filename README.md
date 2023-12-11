
# N5NowTT

This Site is a response to a N5now Technical Test

The site is in NET 6 and with EF and use of Kafka and Elastic

An image is also implemented to run in Docker

## 🚀 About Me
Solution Architect / Manager  with extensive experience in Development in .NET  environments, I also work with PHP / MySQL

I have extensive experience and successful projects with Laravel, PHP/Mysql and Javascript as well as in ASP.NET with AngularJs, ExtJS, Web Forms, MVC and Windows Forms

Father/Husband/Martial Artist

## API Reference

#### Get all items

```http
  GET /GetPermissions
```



#### PUT item

```http
  PUT /ModifyPermission

  {
  "id": 0,
  "nombreEmpleado": "string",
  "apellidoEmpleado": "string",
  "fechaPermiso": "2023-12-10T01:46:55.548Z",
  "tipoPermiso": 0
}
```


#### POST item

```http
  POST /RequestPermission

  {
  "id": 0,
  "nombreEmpleado": "string",
  "apellidoEmpleado": "string",
  "fechaPermiso": "2023-12-10T01:46:55.548Z",
  "tipoPermiso": 0
}
```



## Deployment

To deploy this project run

Use local kafka and elastic enviroments and use 

dot net run N5NowTT.WebApi



## Environment Variables

you can modify the following environment variables to your appsettings.json file

```"ConnectionStrings": {
    "Default": "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=N5NowPermissionDB;Integrated Security=True"
  },
  "Elasticsearch": {
    "Uri": "http://localhost:9200",
    "DefaultIndex": "n5nowindex",
    "user": "elastic",
    "pwd": "yk_*OYadYtcZErxe2Zce"
  },
  "Kafka": {
    "Uri": "localhost:9092",
    "topic": "n5nowtopic"
  },```


