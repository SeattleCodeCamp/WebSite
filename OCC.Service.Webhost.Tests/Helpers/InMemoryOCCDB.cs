using OCC.Data;

namespace OCC.Service.Webhost.Tests.Helpers
{
    public class InMemoryOCCDB : OCCDB
    {
        public InMemoryOCCDB() : base(Effort.DbConnectionFactory.CreateTransient(), true)
        {
        }
    }
}
