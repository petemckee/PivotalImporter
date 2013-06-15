using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Controllers.Models
{
	public class TestPivotalModel
	{
		//public IList<PivotalStory> PivotalStories;
		public IEnumerable<PivotalProject> PivotalProjects;
		public Dictionary<int, IEnumerable<PivotalMembership>> ProjectUsers;
		public Dictionary<int, IEnumerable<PivotalStory>> ProjectStories;
		//public IEnumerable<string> PivotalStoryTypes;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public TestPivotalModel(IEnumerable<PivotalProject> pivotalProjects, Dictionary<int, IEnumerable<PivotalMembership>> projectUsers, Dictionary<int, IEnumerable<PivotalStory>> projectStories)
		{
			PivotalProjects = pivotalProjects;
			ProjectUsers = projectUsers;
			ProjectStories = projectStories;
		}

	}
}