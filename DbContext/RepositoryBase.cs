using ISSystem.DbContext.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISSystem.Models;
using ISSystem.DbContext;
using System.Linq;

namespace Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IModel, new()
    {
        protected readonly AppDbContext _appDbContext;
        public RepositoryBase()
        {
        }

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<T> GetAllItemList()
        {
            return _appDbContext.Set<T>().ToList();
        }

        public List<T> GetActiveItemList()
        {
            return _appDbContext.Set<T>().Where(t => !t.IsDeleted).ToList();
        }

        public T GetItemById(int id)
        {
            return _appDbContext.Set<T>().FirstOrDefault(p => !p.IsDeleted && p.Id == id);
        }

        public void Add(T item)
        {
            _appDbContext.Set<T>().Add(item);
        }

        public void AddRange(List<T> ts)
        {
            _appDbContext.Set<T>().AddRange(ts);
        }


        public void Update(T item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            _appDbContext.Set<T>().Update(item);
        }

        public void Delete(int id)
        {
            T item = GetItemById(id);
            item.IsDeleted = true;
            _appDbContext.Set<T>().Update(item);
        }

        public void Delete(T item)
        {
            item.IsDeleted = true;
            _appDbContext.Set<T>().Update(item);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
