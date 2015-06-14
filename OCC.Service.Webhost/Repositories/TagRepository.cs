using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Management;
using OCC.Data;
using OCC.Service.Webhost.Tools;
using Tag = OCC.Service.Webhost.Services.Tag;

namespace OCC.Service.Webhost.Repositories
{
    public class TagRepository : RepositoryBase
    {
        public TagRepository(OCCDB dbContext)
            : base(dbContext)
        { }

        public IList<Tag> GetTags()
        {
            var result = new List<Tag>();

            var sessionsTags = from t in _dbContext.Tags
                               select new
                               {
                                   t.ID, 
                                   t.TagName, 
                                   SessionsCount = _dbContext.Sessions.Count(s => s.Tag_ID == t.ID)
                               };
            
            foreach (var tag in sessionsTags)
            {
                var tg = new Data.Tag() { ID = tag.ID, TagName = tag.TagName };
                var count = tag.SessionsCount;
                result.Add(tg.Map(count));
            }

            return result;

        }

        public void CreateTag(Tag tag)
        {
            var existingTag = _dbContext.Tags.FirstOrDefault(t => t.TagName == tag.TagName);

            if (existingTag == null)
            {

                var t = new OCC.Data.Tag();
                Mapper.CopyProperties(tag, t);

                _dbContext.Tags.Add(t);
                _dbContext.SaveChanges();
            }
        }

        public Tuple<bool, string> DeleteTag(int tagId)
        {
            
            var tag = _dbContext.Tags.FirstOrDefault(t => t.ID == tagId);
            var message = new StringBuilder();
            var success = false;

            if (tag != null)
            {
                var sessionsForTag = _dbContext.Sessions.Where(s => s.Tag_ID == tag.ID);

                if (!sessionsForTag.Any())
                {
                    _dbContext.Tags.Remove(tag);
                    _dbContext.SaveChanges();
                    success = true;
                }
                else
                {
                    message.Append("Tag in use in following Sessions: ");
                    foreach (var session in sessionsForTag)
                    {
                        message.Append(string.Format("{0}: {1}, ", session.ID, session.Name));
                    }

                    message.Remove(message.Length - 2, 2);
                }

            }
            else
            {
                message.Append(string.Format("Error, TagId: {0} was not found.", tagId));
            }

            return new Tuple<bool, string>(success, message.ToString());

        }

        public bool UpdateTag(Tag tag)
        {
            var result = false;

            var existingTag = _dbContext.Tags.FirstOrDefault(t => t.ID == tag.ID);

            if (existingTag != null)
            {
                existingTag.TagName = tag.TagName;

                _dbContext.SaveChanges();

                result = true;
            }

            return result;

        }

    }
}