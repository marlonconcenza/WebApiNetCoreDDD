using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
         Task Insert(T obj);
         Task Update(T obj);
         Task Remove(int id);
         Task<T> Select(int id);
         Task<IList<T>> SelectAll();
    }
}