using System;
using System.Collections.Generic;
using System.Linq;
using PivotalTrackerAPI.Domain.Enumerations;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Services
{
	public class PivotalTrackerApi : IPivotalTrackerApi
	{
		private PivotalUser user;

		public PivotalTrackerApi()
		{
			user = new PivotalUser(Configuration.PivotalApiToken);
		}

		public IEnumerable<PivotalProject> Projects()
		{
			return user.FetchProjects();
		}

		public IEnumerable<PivotalMembership> ProjectUsers(int projectId)
		{
			var projects = user.FetchProjects();
			var project = projects.Where(x => x.Id == projectId).First();
			// TODO - option to order or not / extension
			var memberships = project.FetchMembers(user).OrderBy(x => x.Person.Name);
			return memberships;
		}

		public IEnumerable<string> StoryTypes()
		{
			IList<string> types = new List<string>();
			foreach (var type in Enum.GetValues(typeof(PivotalStoryType)))
			{
				types.Add(type.ToString());
			}
			return types;
		}
	}
}