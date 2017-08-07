using CC.Service.Webhost.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Data;

namespace ETL
{
    public class EventRepository : RepositoryBase
    {
        public EventRepository() : base(new CC.Data.CCDB())
        {
        }

        public EventRepository(CC.Data.CCDB dbContext)
            : base(dbContext)
        {
        }

        public IList<CC.Data.Event> GetEvents()
        {
            return _dbContext.Events.ToList();
        }
    }
}
