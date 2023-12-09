using N5NowTT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTT.Application
{
    public interface IUnitOfWork
    {
        public Task<Permissions> AddPermissionsAsync(Permissions permission);
        public Task<Permissions> GetPermissionsByIdAsync(int Id);
        public  Task<IEnumerable<Permissions>> GetPermissionsListAsync();
        public  Task<int> UpdatePermissiosnAsync(Permissions Permission);


        public Task<PermissionType> AddPermissionTypeAsync(PermissionType permissiontype);
        public Task<PermissionType> GetPermissionTypeByIdAsync(int Id);
        public Task<IEnumerable<PermissionType>> GetPermissionTypeListAsync();
        public Task<int> UpdatePermissionTypeAsync(PermissionType permissiontype);

    }
}
