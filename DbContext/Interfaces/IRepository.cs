using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSystem.DbContext.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        List<T> GetAllItemList();
        List<T> GetActiveItemList();
        T GetItemById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
