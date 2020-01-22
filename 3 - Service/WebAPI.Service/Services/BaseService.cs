using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using WebAPI.Data.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Service.Services
{
    public class BaseService<T> : IService<T> where T : BaseEntity
    {
        private IOptions<AppSettings> settings;
        private Response response = new Response();
        private BaseRepository<T> repository = null;

        public BaseService(IOptions<AppSettings> settings)
        {
            this.settings = settings;
            this.repository = new BaseRepository<T>(this.settings);
        }
        public async Task<Response> Post<V>(T obj) where V : AbstractValidator<T>
        {
            try
            {
                ValidationResult results = Validate(obj, Activator.CreateInstance<V>());

                if (results.IsValid) {

                    await repository.Insert(obj);

                    response.data = obj;
                    response.success = true;
                }
                else {
                    response.error = results.Errors;
                }
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }

            return response;
        }
        public async Task<Response> Put<V>(T obj) where V : AbstractValidator<T>
        {
            try
            {
                ValidationResult results = Validate(obj, Activator.CreateInstance<V>());

                if (results.IsValid) {

                   await repository.Update(obj);

                    response.data = obj;
                    response.success = true;
                }
                else {
                    response.error = results.Errors;
                }
            }
            catch (Exception ex)
            {
                response.error = ex.Message;
            }

            return response;
        }
        public async Task<Response> Delete(int id)
        {
            try {

                if (id <= 0) throw new ArgumentException("Invalid parameter Id!");

                await repository.Remove(id);

                response.success = true;
            }
            catch (Exception ex) {
                response.error = ex.Message;
            }

            return response;
            
        }
        public async Task<Response> GetAll()
        {
            try {
                response.data = await repository.SelectAll();
                response.success = true;
            }
            catch (Exception ex) {
                response.error = ex.Message;
            }
            
            return response;
        }
        public async Task<Response> Get(int id)
        {
            try {
                
                if (id <= 0) throw new ArgumentException("Invalid parameter Id!");

                response.data = await repository.Select(id);
                response.success = true;
            }
            catch (Exception ex) {
                response.error = ex.Message;
            }
            
            return response;
        }
        private ValidationResult Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
                throw new Exception("Undetected records!");

            return validator.Validate(obj);
        }
    }
}