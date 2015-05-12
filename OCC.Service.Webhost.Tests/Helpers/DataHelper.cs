using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCC.Data;

namespace OCC.Service.Webhost.Tests.Helpers
{
    public static class DataHelper
    {
        public static Event InsertEvent(InMemoryOCCDB context, string name)
        {
            var item = new Event
            {
                Name = name
            };
            context.Events.Add(item);
            return item;
        }

        public static Person InsertPerson(InMemoryOCCDB context, string firstName, string lastName)
        {
            var item = new Person
            {
                FirstName = firstName,
                LastName = lastName
            };
            context.People.Add(item);
            return item;
        }

        public static Track InsertTrack(InMemoryOCCDB context, string name, string description)
        {
            var item = new Track
            {
                Name = name,
                Description = description
            };
            context.Tracks.Add(item);
            return item;
        }
    }
}
