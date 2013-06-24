using System.Web.Mvc;
using PivotalImporter2.Domain.Services;

namespace PivotalImporter2.Web.Controllers
{
	// TODO! Use APIController - and IDependencyResolver (StructureMap.MVC4? from Nuget...)
	[HandleError]
	public class DataController : Controller
    {
	    private readonly IPivotalService pivotalService;

	    public DataController(IPivotalService pivotalService)
	    {
		    this.pivotalService = pivotalService;
	    }

	    public ActionResult ProjectUsers(int projectId)
		{
			var memberships = pivotalService.ProjectUsers(projectId);
		    return Json(memberships, JsonRequestBehavior.AllowGet);
		}
    }
}
