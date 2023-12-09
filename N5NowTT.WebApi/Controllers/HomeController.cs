using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5NowTT.Application;
using N5NowTT.Domain;
using N5NowTT.Infrastructure.Commands;
using N5NowTT.Infrastructure.Queries;
using Nest;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace N5NowTT.WebApi.Controllers
{
    [ApiController]
    [Route("[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]

    public class HomeController : ControllerBase
    {
 

        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private IUnitOfWork _iUoW;
        private ElasticClient _elasticClient;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork iUoW, IMediator mediator, ElasticClient elasticClient)
        {
            _logger = logger;
            _iUoW = iUoW;
            _mediator = mediator;
            _elasticClient = elasticClient; 
        }

     

        [HttpGet(Name = "GetPermissions")]
        public Task<List<GetPermissionQueryResponse>> GetPermissions()
        {
            
            return _mediator.Send(new GetPermissionQuery());

        }
        [HttpPut(Name = "ModifyPermission")]
        public async Task<Permissions> ModifyPermission([FromBody] UpdatePermissionsCommand command)
        {
           
             return await _mediator.Send(command);
        }

        [HttpPost(Name = "RequestPermission")]
        public async Task<Permissions> RequestPermission([FromBody] CreatePermissionsCommand command)
        {
            return await _mediator.Send(command);
        }


    }
}