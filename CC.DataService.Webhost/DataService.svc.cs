using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace OCC.DataService.Webhost
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)] 
    public class DataService : DataService<OrlandoCodeCampEntities>
    {      
        public static void InitializeService(DataServiceConfiguration config)
        {
            config.SetEntitySetAccessRule("Announcements", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Sponsors", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Timeslots", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Tracks", EntitySetRights.AllRead);


            config.SetEntitySetAccessRule("Sessions", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("SessionAttendees", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("People", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Tasks", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("Announcements", EntitySetRights.AllRead);

            config.SetEntitySetPageSize("SessionAttendees", 50);

            config.SetServiceOperationAccessRule("Speakers", ServiceOperationRights.AllRead);

            config.SetEntitySetAccessRule("Tags", EntitySetRights.AllRead);

            // remove before deployment
            config.UseVerboseErrors = true;

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
        [WebGet]
        public IQueryable<Person> Speakers(int eventId)
        {
            return  CurrentDataSource.People.Where(p=>p.Sessions.Any(s=>s.Event_ID == eventId));
        }



    }
}
