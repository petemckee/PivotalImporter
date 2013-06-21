using System;
using PivotalImporter2.Domain.Security;
using PivotalTrackerAPI.Domain.Model;

namespace PivotalImporter2.Domain.Services
{
	public class PivotalMembershipService : IMembershipService
	{

		private readonly ISessionService sessionService;

		public PivotalMembershipService(ISessionService sessionService)
		{
			this.sessionService = sessionService;
		}

		

		public PivotalUser ValidateUser(string userName, string password)
		{
			// Check Pivotal
			try
			{
				var user = PivotalUser.GetUserFromCredentials(userName, password);

				// Clear cache items
				

				return (user);
			}
			catch (Exception e)
			{
				return null;
			}
			
		}

		public string ApiToken()
		{
			return sessionService.ApiToken();
		}

		public PivotalUser CurrentUser()
		{
			return new PivotalUser(sessionService.ApiToken());
		}
		
	}
}
