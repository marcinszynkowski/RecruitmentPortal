using System.Collections.Generic;
using RP.Entities.Account;

namespace RP.AccountModule.Interfaces
{
    public interface IRoleService
    {
        void CreateRole(string roleName, string description);
        void DeleteRole(string roleName);
        List<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        Role GetRoleByName(string roleName);
        string GetRoleDescription(int roleId);
        int GetRoleId(string roleName);
        string GetRoleName(int roleId);
        bool IsRoleExists(string roleName);
        void SetRoleDescription(int roleId, string roleDescription);
        void SetRoleName(int roleId, string roleName);
    }
}