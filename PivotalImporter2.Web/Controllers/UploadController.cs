using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PivotalImporter2.Domain.Services;
using PivotalImporter2.Web.Controllers.Models;
using PivotalTrackerAPI.Domain.Enumerations;
using PivotalTrackerAPI.Domain.Model;
using IMembershipService = PivotalImporter2.Domain.Security.IMembershipService;

namespace PivotalImporter2.Web.Controllers
{
	[HandleError]
	public class UploadController : Controller
	{
		private readonly IPivotalService pivotalService;
		private readonly IMembershipService membershipService;
		private readonly IPivotalExcelService pivotalExcelService;

		public UploadController(IPivotalService pivotalService, IMembershipService membershipService, IPivotalExcelService pivotalExcelService)
		{
			this.pivotalService = pivotalService;
			this.membershipService = membershipService;
			this.pivotalExcelService = pivotalExcelService;
		}

		public ActionResult Index()
		{
			return View(indexViewModel());
		}

		private UploadViewModel indexViewModel()
		{
			// TODO ! Model Validation of form
			var projects = pivotalService.Projects();
			return new UploadViewModel(new List<PivotalStory>(), projects, Enumerable.Empty<PivotalMembership>(), Enumerable.Empty<string>(), -1, null, null);
		}

		/// <summary>
		/// Accepts uploaded file and extracts PivotalStories ready to upload and display for confirmation
		/// </summary>
		/// <param name="file"></param>
		/// <param name="projectId"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Confirm(UploadFileModel uploadModel)
		{
			if (!ModelState.IsValid)
			{
				return View("index", indexViewModel());
			}

			var file = uploadModel.File;
			var projectId = uploadModel.ProjectId;

			var genericLabels = String.Empty;
			var ext = file.FileName.Split('.').Last();
			var fileName = DateTime.Now.ToString("yyMMdd_hhmmss_") + Guid.NewGuid() + "." + ext;
			var path = Path.Combine(Server.MapPath(Domain.Configuration.DefaultUploadUri), fileName);
			file.SaveAs(path);

			//var stories = pivotalExcelService.Stories(Path.Combine(Server.MapPath(Domain.Configuration.DefaultUploadUri), fileName));
			var importInfo = pivotalExcelService.ImportInfo(Path.Combine(Server.MapPath(Domain.Configuration.DefaultUploadUri), fileName));
			var stories = importInfo.Stories;
			var projects = pivotalService.Projects();
			var project = projects.Where(p => p.Id == projectId).FirstOrDefault();
			var memberships = pivotalService.ProjectUsers(projectId);
			var storyTypes = pivotalService.StoryTypes();

			var model = new UploadViewModel(
				stories.ToList()
				, projects
				, memberships
				, storyTypes
				, projectId
				, project.Name
				, importInfo.GenericLabels
				);

			return View(model); 
		}

		
		/// <summary>
		/// take stories JSON from confirmation page - Save to Pivotal
		/// </summary>
		/// <param name="stories"></param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult Publish(List<PivotalStory> stories, int? projectId)
		{
			//pivotalService.SaveStories(stories, projectId);
			// TODO! Update and use pivotalService method
			var proj = pivotalService.Projects().Where(x => x.Id == projectId).FirstOrDefault();
			if (proj != null)
			{
				foreach (var story in stories)
				{
					var st = new PivotalStory(PivotalStoryType.feature, story.Name, story.Description);
					st.Estimate = story.Estimate;
					st.Owner = story.Owner;
					st.Requestor = story.Requestor;
					st.Labels = story.Labels;

					proj.AddStory(membershipService.CurrentUser(), st, false);
				}
			}

			//var result = new JsonResult();
			return Json( new { ProjectId = projectId });
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ActionResult Success(int projectId)
		{
			var projects = pivotalService.Projects();
			var projUsers = new List<PivotalMembership>();
			var projStories = new List<PivotalStory>();

			var project = projects.Where(p => p.Id == projectId).FirstOrDefault();
			var stories = pivotalService.ProjectStories(project.Id.Value);

			projStories = stories.ToList();
			projUsers = project.FetchMembers(membershipService.CurrentUser()).ToList();

			return View(new UploadSuccessModel(project, projUsers, projStories));
		}

	}
}
