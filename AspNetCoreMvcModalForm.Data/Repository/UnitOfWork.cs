using AspNetCoreMvcModalForm.Business.Intefaces;
using AspNetCoreMvcModalForm.Data.Context;

namespace AspNetCoreMvcModalForm.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataDbContext _dbContext;
        private bool disposed;
        public UnitOfWork(DataDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }
    }
}
