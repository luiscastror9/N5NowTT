using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N5NowTT.Domain;

namespace N5NowTT.Infrastructure
{
    public class MiDbContext : DbContext


    {
        public MiDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<MiDbContext> options) : base(options) { }
       
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }


    }
}
