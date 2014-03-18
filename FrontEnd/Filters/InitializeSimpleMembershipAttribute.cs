using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using CrowdFunding.Models;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CrowdFunding.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private const string ConnectionString = "DefaultConnection";

        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        //public para llamarlo desde globalaasax
        public class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection(ConnectionString, "UserProfile", "UserId", "UserName", autoCreateTables: true);

                    CreateDefaultRolesAndUsers();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            private void CreateDefaultRolesAndUsers()
            {
                var roles = (SimpleRoleProvider)Roles.Provider;

                if (!roles.RoleExists("Admin"))
                {
                    // Create evertything we need
                    roles.CreateRole("Admin");
                    roles.CreateRole("PowerUser");
                    roles.CreateRole("User");

                    var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString);
                    var cmd = new SqlCommand();

                    cmd.CommandText = "alter table userprofile add " +
                                        "Email nvarchar(200) not null," +
                                        "IsActive bit not null," +
                                        "IsDeleted bit not null," +
                                        "WorkDays int not null," +
                                        "WorkHourFrom char(5) not null," +
                                        "WorkHourTo char(5) not null," +
                                        "RestrictAccessByIP bit not null;";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    sqlConnection.Close();

                    foreach (var username in new[] { "damianl", "paolap", "javiert", "matiask", "martink", "diegos", "leok", "claudiaz", "samyd", "alexr", "jessief", "alea", "carlosz", "nandok", "arik", "zackyk", "sebad", "mariano", "idan", "yaniva", "baruchf" })
                    {
                        WebSecurity.CreateUserAndAccount(username, "dddddd", GetUserProfileExtradata(username), false);
                        roles.AddUsersToRoles(new[] { username }, new[] { "User" });
                    }

                    roles.AddUsersToRoles(new[] { "damianl" }, new[] { "Admin", "PowerUser", "User" });
                }
            }

            private object GetUserProfileExtradata(string username)
            {
                var r = new Random();

                return new
                {
                    Email = username + "@gmail.com",
                    IsActive = (r.Next(100) < 90),
                    IsDeleted = (r.Next(100) < 15),
                    WorkDays = r.Next(0, 128),
                    WorkHourFrom = "00:00",
                    WorkHourTo = "23:59",
                    RestrictAccessByIP = (r.Next(100) > 75),
                };
            }
        }
    }
}
