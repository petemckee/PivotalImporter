using PivotalImporter2.Domain.Cacheing;
using PivotalImporter2.Domain.Security;
using PivotalImporter2.Domain.Services;
using StructureMap;

namespace PivotalImporter2.Domain.StructureMap
{
	public class BootStrapper
	{
		public static void Bootstrap()
		{
			// Get an iValidator object from structureMap
			// using ObjectFactory.GetInstance<IValidator>();

			// initialise the staic objectFactory container
			ObjectFactory.Initialize(x =>
			{
				//x.For<IValidator>().TheDefaultIsConcreteType<IValidator>();

				x.For<ISessionService>().Use<SessionService>();
				x.For<IPivotalTrackerApi>().Use<PivotalTrackerApi>();

				x.For<IMembershipService>().Use<PivotalMembershipService>();
				x.For<IPivotalExcelService>().Use<PivotalExcelService>();
				//x.For<IPivotalTrackerDotNetMembershipService>().Use<MockPivotalMembershipService>();

				x.For<IPivotalService>().Use<PivotalService>();
				x.For<ICacheProvider>().Use<DefaultCacheProvider>();

				x.For<IFormsAuthenticationService>().Use<FormsAuthenticationService>();
			});

		}
	}
}
