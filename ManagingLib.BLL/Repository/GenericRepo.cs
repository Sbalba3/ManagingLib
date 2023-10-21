using ManagingLib.DAL.Context;
using ManagingLib.DAL.Models;
using MangaingLib.BLL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaingLib.BLL.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly ManagingContext _managingContext;

        public GenericRepo(ManagingContext managingContext)
        {
            _managingContext = managingContext;
        }
        public async Task Add(T item)
        {
            await _managingContext.Set<T>().AddAsync(item);
        }

        public void Delete(T item)
        {
            _managingContext.Set<T>().Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Book))
            {
                return (IEnumerable<T>)await _managingContext.Books.Include(x => x.Author).Include(x=>x.Genre).ToListAsync();
            }
            return await _managingContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
           return await _managingContext.Set<T>().FindAsync(id);
        }

        public void Update(T item)
        {
            _managingContext.Entry(item).State = EntityState.Modified;
        }
        public async Task Save()
        {
            await _managingContext.SaveChangesAsync();
        }
        public void Dispose()
         => _managingContext.Dispose();
    }

}
