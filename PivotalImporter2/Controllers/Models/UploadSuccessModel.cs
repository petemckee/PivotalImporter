using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Controllers.Models
{
	public class UploadSuccessModel
	{
		public PivotalProject PivotalProject;
		public IEnumerable<PivotalMembership> PivotalMemberships;
		public IEnumerable<PivotalStory> ProjectStories;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public UploadSuccessModel(PivotalProject pivotalProject, IEnumerable<PivotalMembership> pivotalMemberships, IEnumerable<PivotalStory> projectStories)
		{
			PivotalProject = pivotalProject;
			PivotalMemberships = pivotalMemberships;
			ProjectStories = projectStories;
		}
	}
}