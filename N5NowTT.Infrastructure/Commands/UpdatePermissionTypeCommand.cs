using MediatR;
using N5NowTT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTT.Infrastructure.Commands
{
    public  class UpdatePermissionTypeCommand: IRequest<PermissionType>
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }
    }
    public class UpdatePermissionTypeCommandHandler : IRequestHandler<UpdatePermissionTypeCommand,PermissionType>
    {
        private readonly MiDbContext _context;

        public UpdatePermissionTypeCommandHandler(MiDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionType> Handle(UpdatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            var newPermission = new PermissionType
            {
                Id = request.Id,
                Descripcion = request.Descripcion,

            };

            _context.PermissionTypes.Update(newPermission);

            await _context.SaveChangesAsync();

            return newPermission;
        }
    }
}
