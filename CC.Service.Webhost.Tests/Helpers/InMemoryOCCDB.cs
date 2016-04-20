using CC.Data;

namespace CC.Service.Webhost.Tests.Helpers
{
    public class InMemoryOCCDB : CCDB
    {
        public InMemoryOCCDB() : base(Effort.DbConnectionFactory.CreateTransient(), true)
        {
        }
    }
}
