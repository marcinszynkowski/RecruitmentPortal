using RP.AccountModule.Interfaces;
using RP.AccountModule.Services;
using RP.Data.Context;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RP.Web.Security
{
    public class RPRoleProvider : RoleProvider
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public RPRoleProvider()
        {
            _userService = new UserService(new RPDbContext());
            _roleService = new RoleService(new RPDbContext());
        }

        public override string[] GetRolesForUser(string email)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userRole = new string[] { _roleService.GetRoleName(_userService.GetUserRoleId(email)) };

            return userRole.ToArray();
        }

        #region Overrides of Role Provider
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override bool IsUserInRole(string email, string roleName) => throw new NotImplementedException();

        public override void AddUsersToRoles(string[] usernames, string[] roleNames) => throw new NotImplementedException();

        public override void CreateRole(string roleName) => throw new NotImplementedException();

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) => throw new NotImplementedException();

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) => throw new NotImplementedException();

        public override string[] GetAllRoles() => throw new NotImplementedException();

        public override string[] GetUsersInRole(string roleName) => throw new NotImplementedException();

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) => throw new NotImplementedException();

        public override bool RoleExists(string roleName) => throw new NotImplementedException();
        #endregion
    }
}