using MediatR;
using N5NowTT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTT.Infrastructure.Commands
{
    public class CreatePermissionTypeCommand : IRequest<PermissionType>
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
    }
    public class CreatePermissionTypeCommandHandler : IRequestHandler<CreatePermissionTypeCommand,PermissionType>
    {
        private readonly MiDbContext _context;

        public CreatePermissionTypeCommandHandler(MiDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionType> Handle(CreatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            var newPermission = new PermissionType
            {
                Descripcion = request.Descripcion,
             
            };

            _context.PermissionTypes.Add(newPermission);

             await _context.SaveChangesAsync();
            return _context.PermissionTypes.LastOrDefault();
            //return Unit.Value;
        }
    }

}
