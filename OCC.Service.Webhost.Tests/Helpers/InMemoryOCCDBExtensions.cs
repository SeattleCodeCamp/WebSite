using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OCC.Data;

namespace OCC.Service.Webhost.Tests.Helpers
{
    public static class InMemoryOCCDBExtensions
    {
        public static InMemoryOCCDB WithEvent(this InMemoryOCCDB context, string name)
        {
            DataHelper.InsertEvent(context, name);
            context.SaveChanges();
            return context;
        }

        public static InMemoryOCCDB WithPerson(this InMemoryOCCDB context, string firstName, string lastName)
        {
            DataHelper.InsertPerson(context, firstName, lastName);
            context.SaveChanges();
            return context;
        }
    }
}
