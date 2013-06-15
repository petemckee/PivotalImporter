using System.Web.Mvc;
using PivotalImporter2.Domain.Json;
using PivotalImporter2.Domain.PivotalTracker;
using PivotalImporter2.Domain.Services;
using StructureMap;

namespace PivotalImporter2.Web.Controllers.Models
{
	[HandleError]
	[Authorize]
	public class PivotalDataController : Controller
	{
		private IPivotalTrackerApi trackerApi;

		public PivotalDataController()
		{
			trackerApi = ObjectFactory.GetInstance<IPivotalTrackerApi>();
		}


		public JsonResult Index()
		{

			var json = new JsonResult();

			json.Data = new { Success = true };
			json.ContentType = "application/json";
			json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

			return (json);
		}

		
		public JsonResult Projects()
		{
			var result = new JsonSuccessResult();
			var projects = trackerApi.Projects();
			result.Data = projects;
			return result;
		}

		public JsonResult ProjectUsers(int projectId)
		{
			var result = new JsonSuccessResult();

			/* -- TODO! Cache retrival from server!
			var users = trackerApi.ProjectUsers(projectId);*/
			var users = new[]
			            	{
			            		new
			            			{
			            				Id = 2688931,
			            				Person = new {Email = "p15tol@yahoo.com", Name = "Pete", Initials = "PE"},
			            				Role = "Owner"
			            			}
			            	};

			result.Data = users;
			return result;		
		}
	}
}