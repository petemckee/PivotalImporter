using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Services
{
	public interface IPivotalTrackerApi
	{
		IEnumerable<PivotalProject> Projects();
		IEnumerable<PivotalMembership> ProjectUsers(int projectId);
		IEnumerable<string> StoryTypes();
	}
}