using MediatR;
using Microsoft.EntityFrameworkCore;
using N5NowTT.Domain;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTT.Infrastructure.Queries
{


    public class GetPermissionQuery: MediatR.IRequest<List<GetPermissionQueryResponse>>
    {
    }

    public class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, List<GetPermissionQueryResponse>>
    {
        private readonly MiDbContext _context;

        public GetPermissionQueryHandler(MiDbContext context, IElasticClient elasticClient)
        {
            _context = context;
        }

        public Task<List<GetPermissionQueryResponse>> Handle(GetPermissionQuery request, CancellationToken cancellationToken) =>
            _context.Permissions
                .AsNoTracking()
                .Select(s => new GetPermissionQueryResponse
                {
                    NombreEmpleado = s.NombreEmpleado,
                    ApellidoEmpleado = s.ApellidoEmpleado,
                    FechaPermiso = s.FechaPermiso,
                    TipoPermiso=s.TipoPermiso
                })
                .ToListAsync();
    }

    public class GetPermissionQueryResponse
    {
        public int Id { get; set; }
        public string? NombreEmpleado { get; set; }

        public string? ApellidoEmpleado { get; set; }
        public DateTime FechaPermiso { get; set; }

        public PermissionType? TipoPermiso { get; set; }
    }

}
