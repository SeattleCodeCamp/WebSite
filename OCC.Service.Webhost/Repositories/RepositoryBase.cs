using OCC.Data;

namespace OCC.Service.Webhost.Repositories
{
    public class RepositoryBase
    {
        protected readonly OCCDB _dbContext;

        public RepositoryBase(OCCDB dbContext)
        {
            _dbContext = dbContext;
        }
    }
}