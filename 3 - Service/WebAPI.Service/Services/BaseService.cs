using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using WebAPI.Data.Repository;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Service.Services {
    public class BaseService<T> : IService<T> where T : BaseEntity {
        private IOptions<AppSettings> settings;
        private BaseRepository<T> repository = null;
        public BaseService (IOptions<AppSettings> settings) {
            this.settings = settings;
            this.repository = new BaseRepository<T> (this.settings);
        }
        public async Task<T> Post<V> (T obj) where V : AbstractValidator<T> {

            ValidationResult results = Validate (obj, Activator.CreateInstance<V> ());

            if (!results.IsValid)
                throw this.ReturnErros (results.Errors);

            await repository.Insert (obj);

            return obj;
        }
        public async Task<T> Put<V> (T obj) where V : AbstractValidator<T> {

            ValidationResult results = Validate (obj, Activator.CreateInstance<V> ());

            if (!results.IsValid)
                throw this.ReturnErros (results.Errors);

            await repository.Update (obj);

            return obj;
        }
        public async Task Delete (int id) {

            if (id <= 0) 
                throw new ArgumentException ("Invalid parameter Id!");

            await repository.Remove (id);
        }
        public async Task<IList<T>> GetAll () => await repository.SelectAll();
        public async Task<T> Get (int id) {

            if (id <= 0) 
                throw new ArgumentException ("Invalid parameter Id!");

            return await repository.Select(id);
        }
        private ValidationResult Validate (T obj, AbstractValidator<T> validator) {

            if (obj == null)
                throw new Exception ("Undetected records!");

            return validator.Validate (obj);
        }
        private AggregateException ReturnErros (IList<ValidationFailure> erros) {

            IList<Exception> exceptions = null;

            if (erros != null) {

                exceptions = new List<Exception> ();

                foreach (var item in erros) {
                    exceptions.Add (new Exception (item.ErrorMessage));
                }
            }

            return new AggregateException (exceptions);
        }
    }
}