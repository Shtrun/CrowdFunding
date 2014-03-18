using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using CrowdFunding.Filters;
using CrowdFunding.Models;
using LoggerProject;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace CrowdFunding.Controllers
{
    [Authorize]
    [BreezeController]
    public class AngelikooController : ApiController
    {
        private readonly AngelikooRepository _repository;

        public AngelikooController()
        {
            _repository = new AngelikooRepository(User);
        }

        // GET ~/api/Todo/Metadata 
        [HttpGet]
        [AllowAnonymous]
        public string Metadata()
        {
            return _repository.Metadata();
        }

        // GET ~/api/Angelikoo/Companies
        [HttpGet]
        [AllowAnonymous]
        public IQueryable<Company> Companies()
        {
            Thread.Sleep(1000); //@@@ take some time

            return _repository.Companies;/*.Select(item => new Company()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Logo = item.Logo,
                    Description = item.Description,
                    Goal = item.Goal,
                    Raised = item.Raised,
                    Status = item.Status,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate
                }) */
        }

        // POST ~/api/Todo/SaveChanges
        [HttpPost]
        [ValidateHttpAntiForgeryToken]
        [Authorize(Roles = "Admin, PowerUser")]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            var context = new UsersContext();
            var username = User.Identity.Name;
            var user = context.UserProfiles.SingleOrDefault(u => u.UserName == username);

            TheLogger.Write(string.Format("User {0} saved changes.", username), TraceEventType.Information);


            return _repository.SaveChanges(saveBundle);
        }
    }
}