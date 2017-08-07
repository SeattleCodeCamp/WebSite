using CC.Service.Webhost.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Data;

namespace ETL
{
    public class SessionRepository : RepositoryBase
    {
        public SessionRepository() : base(new CC.Data.CCDB())
        {
        }

        public SessionRepository(CC.Data.CCDB dbContext)
            : base(dbContext)
        {
        }

        public IList<CC.Data.Session> GetSpeakerSessions(int speakerId)
        {
            return _dbContext.Sessions.Where(s => s.Speaker_ID == speakerId).ToList();
        }

        public void CreateSession(CC.Data.Session session)
        {
            _dbContext.Sessions.Add(session);
            _dbContext.SaveChanges();
        }

        internal void UpdateSession(int id, CC.Data.Session session)
        {
            var s = _dbContext.Sessions.Find(id);
            s.Name = session.Name;
            s.Description = session.Description;
            s.Level = session.Level;
            s.Location = session.Location;
            s.Status = session.Status;
            s.Tag_ID = session.Tag?.ID;

            _dbContext.SaveChanges();
        }
    }
}
