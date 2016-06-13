using CC.Data;

namespace CC.Service.Webhost.Repositories
{
    public class RepositoryBase
    {
        protected readonly CCDB _dbContext;

        public RepositoryBase(CCDB dbContext)
        {
            _dbContext = dbContext;
        }
    }
}