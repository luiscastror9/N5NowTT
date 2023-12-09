using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using N5NowTT.Application;
using N5NowTT.Domain;

namespace N5NowTT.Infrastructure
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly MiDbContext _dbContext;

        public UnitOfWork(MiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Permissions

        public async Task<Permissions> AddPermissionsAsync(Permissions permission)
        {
            var result = _dbContext.Permissions.Add(permission);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Permissions> GetPermissionsByIdAsync(int Id)
        {
            return await _dbContext.Permissions.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Permissions>> GetPermissionsListAsync()
        {
            return await _dbContext.Permissions.ToListAsync();
        }

        public async Task<int> UpdatePermissiosnAsync(Permissions Permission)
        {
            _dbContext.Permissions.Update(Permission);
            return await _dbContext.SaveChangesAsync();
        }

        #endregion
        #region PermissionType
        public async Task<PermissionType> AddPermissionTypeAsync(PermissionType permissiontype)
        {
            var result = _dbContext.PermissionTypes.Add(permissiontype);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PermissionType> GetPermissionTypeByIdAsync(int Id)
        {
            return await _dbContext.PermissionTypes.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PermissionType>> GetPermissionTypeListAsync()
        {
            return await _dbContext.PermissionTypes.ToListAsync();
        }

        public async Task<int> UpdatePermissionTypeAsync(PermissionType PermissionType)
        {
            _dbContext.PermissionTypes.Update(PermissionType);
            return await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
