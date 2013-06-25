using System.Collections.Generic;
using System.Web;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Web.Controllers.Models
{
	public class UploadViewModel
	{
		public IList<PivotalStory> PivotalStories;
		public IEnumerable<PivotalProject> PivotalProjects;
		public IEnumerable<PivotalMembership> PivotalMemberships;
		public IEnumerable<string> PivotalStoryTypes;
		public int ProjectId;
		public string ProjectName;
		public string GenericLabels;
		public HttpPostedFile File;

		public UploadViewModel()
		{
		}

		public UploadViewModel(IList<PivotalStory> pivotalStories, IEnumerable<PivotalProject> pivotalProjects, IEnumerable<PivotalMembership> pivotalMemberships, IEnumerable<string> pivotalStoryTypes, int projectId, string projectName, string genericLabels)
		{
			PivotalStories = pivotalStories;
			PivotalProjects = pivotalProjects;
			PivotalMemberships = pivotalMemberships;
			PivotalStoryTypes = pivotalStoryTypes;
			ProjectId = projectId;
			ProjectName = projectName;
			GenericLabels = genericLabels;

		}

		public UploadViewModel(IList<PivotalStory> pivotalStories, IEnumerable<PivotalProject> pivotalProjects, IEnumerable<PivotalMembership> pivotalMemberships, IEnumerable<string> pivotalStoryTypes, int projectId, string projectName, string genericLabels, HttpPostedFile file)
		{
			PivotalStories = pivotalStories;
			PivotalProjects = pivotalProjects;
			PivotalMemberships = pivotalMemberships;
			PivotalStoryTypes = pivotalStoryTypes;
			ProjectId = projectId;
			ProjectName = projectName;
			GenericLabels = genericLabels;
			File = file;
		}
	}
}