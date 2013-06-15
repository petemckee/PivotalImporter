namespace PivotalImporter2.Domain.Security
{
	public interface ISessionService
	{
		void SetApiToken(string apiToken);
		string ApiToken();
		void SetUserName(string userName);
		string UserName();
	}
}
