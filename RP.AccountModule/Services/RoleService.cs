using RP.AccountModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Account;
using System.Collections.Generic;
using System.Linq;

namespace RP.AccountModule.Services
{
    public class RoleService : IRoleService
    { 
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public RoleService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public Role GetRoleByName(string roleName)
        {
            return _db.Roles.Where(r => r.Name == roleName).FirstOrDefault();
        }

        public Role GetRoleById(int roleId)
        {
            return _db.Roles.Find(roleId);
        }

        public string GetRoleName(int roleId)
        {
            return _db.Roles.Find(roleId).Name;
        }

        public int GetRoleId(string roleName)
        {
            return GetRoleByName(roleName).Id;
        }

        public string GetRoleDescription(int roleId)
        {
            return GetRoleById(roleId).Description;
        }

        public List<Role> GetAllRoles()
        {
            return _db.Roles.ToList();
        }
        #endregion

        #region Set
        public void SetRoleName(int roleId, string roleName)
        {
            Role role = GetRoleById(roleId);
            role.Name = roleName;
            _db.SaveChanges();
        }

        public void SetRoleDescription(int roleId, string roleDescription)
        {
            Role role = GetRoleById(roleId);
            role.Description = roleDescription;
            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateRole(string roleName, string description)
        {
            Role role = new Role
            {
                Name = roleName,
                Description = description
            };

            _db.Roles.Add(role);
            _db.SaveChanges();
        }
        #endregion

        #region Delete
        public void DeleteRole(string roleName)
        {
            Role role = GetRoleByName(roleName);

            _db.Roles.Remove(role);
            _db.SaveChanges();
        }
        #endregion

        #region Is
        public bool IsRoleExists(string roleName)
        {
            return GetRoleByName(roleName) != null;
        }
        #endregion
    }
}