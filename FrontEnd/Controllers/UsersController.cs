using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using CrowdFunding.Filters;
using CrowdFunding.Models;
using LoggerProject;
using Newtonsoft.Json.Linq;
using WebMatrix.WebData;
using System.Collections.Generic;

namespace CrowdFunding.Controllers
{
    [Authorize]
    [BreezeController]
    public class UsersController : ApiController
    {
        private readonly UsersRepository _repository;

        public UsersController()
        {
            _repository = new UsersRepository(User);
        }

        // GET ~/api/Users/Metadata 
        [HttpGet]
        [AllowAnonymous]
        public string Metadata()
        {
            return _repository.Metadata();
        }

        // POST ~/api/Users/SaveChanges
        [HttpPost]
        [ValidateHttpAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);

            TheLogger.Write(string.Format("User {0} saved changes.", username), TraceEventType.Information);

            return _repository.SaveChanges(saveBundle);
        }

        // GET ~/api/Users/UserProfiles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IQueryable<UserProfile> UserProfiles()
        {
            return _repository.UserProfiles;
        }

        // GET ~/api/Users/Roles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IQueryable<Role> Roles()
        {
            return _repository.Roles;
        }

        // GET ~/api/Users/UsersInRoles
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public List<UsersInRoles> UsersInRoles()
        {
            return _repository.UsersInRoles;
        }

        //// GET ~/api/Users/UsersInRoles
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public IQueryable<UsersInRoles> UsersInRoles()
        //{
        //    return null;
        //    //return _repository.UsersInRoles;
        //    ////@@@ I couldn't get EF+SimpleMembership to work together. I'll need to update this table directly through SQL statements

        //    //var usersInRoles = new List<UsersInRoles>();

        //    //var roles = (SimpleRoleProvider)System.Web.Security.Roles.Provider;                                   
        //    //foreach (var userProfile in _repository.UserProfiles)
        //    //{
        //    //    var rolesForUser = roles.GetRolesForUser(userProfile.UserName);
        //    //    foreach (var roleForUser in rolesForUser)
        //    //    {
        //    //        usersInRoles.Add(new Models.UsersInRoles() { UserId = userProfile.UserId, Role = roleForUser });
        //    //    }                
        //    //}

        //    //return usersInRoles;
        //}

        // GET ~/api/Users/Membership
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IQueryable<Membership> Memberships()
        {
            return _repository.Memberships;
        }

        // GET ~/api/Users/OAuthMembership
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IQueryable<OAuthMembership> OAuthMemberships()
        {
            return _repository.OAuthMemberships;
        }
    }
}