namespace PivotalImporter2.Domain.Security
{
	public interface IFormsAuthenticationService
	{
		void SignIn(string userName, bool createPersistentCookie, string apiToken);
		void SignOut();
		string ApiToken();
	}
}