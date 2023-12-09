using MediatR;
using N5NowTT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTT.Infrastructure.Commands
{
    public class CreatePermissionsCommand : IRequest<Permissions>
    {
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }

        public string? ApellidoEmpleado { get; set; }
        public DateTime FechaPermiso { get; set; }

        public PermissionType? TipoPermiso { get; set; }
    }

    public class CreatePermissionsCommandHandler : IRequestHandler<CreatePermissionsCommand,Permissions>
    {
        private readonly MiDbContext _context;

        public CreatePermissionsCommandHandler(MiDbContext context)
        {
            _context = context;
        }

        public async Task<Permissions> Handle(CreatePermissionsCommand request, CancellationToken cancellationToken)
        {
            var newPermission = new Permissions
            {
                NombreEmpleado = request.NombreEmpleado,
                ApellidoEmpleado = request.ApellidoEmpleado,
                FechaPermiso = request.FechaPermiso,
                TipoPermiso = request.TipoPermiso
            };

            _context.Permissions.Add(newPermission);

            await _context.SaveChangesAsync();

            return _context.Permissions.LastOrDefault();
        }
    }
}
