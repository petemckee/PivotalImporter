using System.Collections.Generic;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Services
{
	public interface IPivotalService
	{
		IEnumerable<PivotalProject> Projects();
		IEnumerable<PivotalProject> Projects(bool refreshCache);
		IEnumerable<PivotalMembership> ProjectUsers(int projectId);
		IEnumerable<PivotalMembership> ProjectUsers(int projectId, bool refreshCache);
		IEnumerable<PivotalStory> ProjectStories(int projectId);
		IEnumerable<PivotalStory> ProjectStories(int projectId, bool refreshCache);
		IEnumerable<string> StoryTypes();
		IEnumerable<PivotalStory> SaveStories(List<PivotalStory> stories, int? projectId);
	}
}