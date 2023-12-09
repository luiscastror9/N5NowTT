using MediatR;
using N5NowTT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace N5NowTT.Infrastructure.Commands
{
    public class UpdatePermissionsCommand: IRequest<Permissions>
    {
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }

        public string? ApellidoEmpleado { get; set; }
        public DateTime FechaPermiso { get; set; }

        public int TipoPermiso { get; set; }
    }
    public class UpdatePermissionsCommandHandler : IRequestHandler<UpdatePermissionsCommand,Permissions>
    {
        private readonly MiDbContext _context;

        public UpdatePermissionsCommandHandler(MiDbContext context)
        {
            _context = context;
        }

        public async Task<Permissions> Handle(UpdatePermissionsCommand request, CancellationToken cancellationToken)
        {
            var updatePermission = _context.Permissions.FirstOrDefault(x=>x.Id==request.Id);
            if (updatePermission == null)
                return null;



            updatePermission.Id =  request.Id;
            updatePermission.NombreEmpleado = request.NombreEmpleado ?? updatePermission.NombreEmpleado;
            updatePermission.ApellidoEmpleado = request.ApellidoEmpleado ?? updatePermission.ApellidoEmpleado;
            updatePermission.FechaPermiso = request.FechaPermiso;
            updatePermission.TipoPermiso = request.TipoPermiso !=null?  _context.PermissionTypes.FirstOrDefault(x => x.Id == request.TipoPermiso) : updatePermission.TipoPermiso; 
            

            _context.Permissions.Update(updatePermission);

            await _context.SaveChangesAsync();

            return updatePermission;
        }
    }
}
