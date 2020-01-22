using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
         Task<Response> Post<V>(T obj) where V : AbstractValidator<T>;

        Task<Response> Put<V>(T obj) where V : AbstractValidator<T>;

        Task<Response> Delete(int id);

        Task<Response> GetAll();
        Task<Response> Get(int id);
    }
}