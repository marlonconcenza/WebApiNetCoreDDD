using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T> Post<V>(T obj) where V : AbstractValidator<T>;
        Task<T> Put<V>(T obj) where V : AbstractValidator<T>;
        Task Delete(int id);
        Task<T> Get(int id);
        Task<IList<T>> GetAll();
    }
}