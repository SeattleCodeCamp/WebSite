using System;
using System.Linq;
using CC.Data;

namespace CC.Service.Webhost.Repositories
{
    public class MetadataRepository : RepositoryBase
    {
        public MetadataRepository() : base(new CCDB())
        {
        }
        public MetadataRepository(CCDB dbContext)
            : base(dbContext)
        {
        }

        public string GetValueForKey(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return String.Empty;
            }

            var result = _dbContext.KeyValuePairs
                .Where(x => x.Id.Equals(key))
                .FirstOrDefault();

            return result.Value;
        }
    }
}