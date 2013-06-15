using System;
using System.Web.Security;

namespace PivotalImporter2.Domain.Security
{
	public class FormsAuthenticationService : IFormsAuthenticationService
	{
		private readonly ISessionService sessionService;
		private string _apiToken;

		public FormsAuthenticationService(ISessionService sessionService)
		{
			this.sessionService = sessionService;
		}

		public void SignIn(string userName, bool createPersistentCookie, string apiToken)
		{
			if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");

			_apiToken = userName;
			FormsAuthentication.SetAuthCookie(userName, true);

			sessionService.SetApiToken(apiToken);
			sessionService.SetUserName(userName);
		}

		public void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		public string UserName()
		{
			return sessionService.UserName();
		}

		public string ApiToken()
		{
			return sessionService.ApiToken();
		}
	}
}