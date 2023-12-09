using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using N5NowTT.Application;
using N5NowTT.Domain;
using N5NowTT.Infrastructure.Commands;
using N5NowTT.Infrastructure.Queries;
using N5NowTT.WebApi.Controllers;
using Nest;

namespace N5NowTT.UnitTest
{
    public class UnitTestN5NowTT
    {
        private readonly Mock<List<GetPermissionQueryResponse>> _getPermissionQuery;
        private readonly Mock<CreatePermissionsCommand> _createPermissionsCommand;
        private readonly Mock<UpdatePermissionsCommand> _updatePermissionsCommand;
        private HomeController _controller;

        private Mock<ILogger<HomeController>> _logger;
        private Mock<IMediator> _mediator;
        private Mock<IUnitOfWork> _iUoW;
        private Mock<ElasticClient> _elasticClient;
        public UnitTestN5NowTT()
        {
          
        }
        [Fact]
        public void GetPermissions_Permissions()
        {
            var permissionList = GetPermissionQueryResponse();
            _logger = new Mock<ILogger<HomeController>>();
            _mediator = new Mock<IMediator>();
            _iUoW = new Mock<IUnitOfWork>();    
            _elasticClient = new Mock<ElasticClient>();

            _controller = new HomeController(_logger.Object, _iUoW.Object, _mediator.Object, _elasticClient.Object);
            //act
            var permissionResult = _controller.GetPermissions();
            //assert
            Assert.NotNull(permissionResult);
//            Assert.Equal(GetPermissionQueryResponse().Count(), permissionResult.Result.Count());
//            Assert.Equal(GetPermissionQueryResponse().ToString(), permissionResult.ToString());
            Assert.True(permissionList.Equals(permissionResult));
        }

        private List<GetPermissionQueryResponse> GetPermissionQueryResponse()
        {
            List<GetPermissionQueryResponse> permissionsData = new List<GetPermissionQueryResponse>
        {
            new GetPermissionQueryResponse
            {
                Id = 1,
                NombreEmpleado = "IPhone",
                ApellidoEmpleado = "IPhone 12",
                FechaPermiso = DateTime.Now,
                TipoPermiso = this.GetPermissionTypeResponse()
            },
             new GetPermissionQueryResponse
            {
                Id = 2,
                NombreEmpleado = "Laptop",
                ApellidoEmpleado = "HP Pavilion",
                FechaPermiso = DateTime.Now,
                TipoPermiso = this.GetPermissionTypeResponse()
            },
             new GetPermissionQueryResponse
            {
                Id = 3,
                NombreEmpleado = "TV",
                ApellidoEmpleado = "Samsung Smart TV",
                FechaPermiso = DateTime.Now,
                TipoPermiso = this.GetPermissionTypeResponse()
            },
        };
            return permissionsData;
        }

        private PermissionType GetPermissionTypeResponse()
        {
            return new PermissionType { Id = 1, Descripcion = "Administrator" };
        }
    }
}