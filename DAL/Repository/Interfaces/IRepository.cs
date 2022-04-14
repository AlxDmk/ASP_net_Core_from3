using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Lesson3.DAL.Repository.Interfaces
{
    public interface IRepository <T>
    {
        Task<T> Get(int id);
        Task Add(T item);
        Task Update(T item);
        Task Delete(int id);
        Task<T> GetByName(string name);
        Task<IEnumerable<T>> Select(int skip, int take);
    }
}