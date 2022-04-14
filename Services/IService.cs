using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lesson3.Services
{
    public interface IService<T> where T : class
    {
        Task Create(T item);
        Task Update(T item);
        Task Delete(int id);
        Task<T> Get(int id);
        Task<T> GetByName(string name);
        Task<IList<T>> Select(int skip, int take);

    }
}