using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Security
{
	public interface IMembershipService
	{
		PivotalUser ValidateUser(string userName, string password);
		string ApiToken();
		PivotalUser CurrentUser();
	}
}