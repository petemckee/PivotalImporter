using System;
using System.Web;

namespace PivotalImporter2.Domain.Security
{
	public class SessionService : ISessionService
	{
		private string _apiToken = "ApiToken";
		private string _userName = "UserName";
		private int expiresLengthYears = 1;

		private void SetCookie(string cookieName, string value)
		{
			var cookie = new HttpCookie(cookieName);
			cookie.Expires = DateTime.Now.AddYears(expiresLengthYears);
			cookie.Value = value;
			HttpContext.Current.Response.Cookies.Add(cookie);
		}
		
		private string GetCookie(string cookieName)
		{
			var cookie = HttpContext.Current.Request.Cookies.Get(cookieName);
			return (cookie != null) ? cookie.Value : null;
		}

		public void SetApiToken(string token)
		{			
			SetCookie(_apiToken, token);
		}

		public string ApiToken()
		{
			return GetCookie(_apiToken);
		}

		public void SetUserName(string userName)
		{
			//HttpContext.Current.Session[this._userName] = userName;
			SetCookie(_userName, userName);
		}

		public string UserName()
		{
			//return (HttpContext.Current.Session[_userName] != null) ? HttpContext.Current.Session[_userName].ToString() : string.Empty;
			return GetCookie(_userName);
		}
	}
}
