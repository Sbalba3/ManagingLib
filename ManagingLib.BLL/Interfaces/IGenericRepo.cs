using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaingLib.BLL.Interfaces
{
    public interface IGenericRepo<T>where T : class 
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task Add(T item);
        void Update(T item);
        void Delete(T item);
        Task Save();
    }
}
