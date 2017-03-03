using DSitemapTester.DAL.EFContext;
using DSitemapTester.DAL.Interfaces;
using System;

namespace DSitemapTester.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SitemapContext db;

        private bool disposed = false;

        public EFUnitOfWork(SitemapContext context)
        {
            this.db = context;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new BaseRepository<T>(this.db);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
