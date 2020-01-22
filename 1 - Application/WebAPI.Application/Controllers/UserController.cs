using System;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Services;
using WebAPI.Service.Validators;
using WebAPI.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebAPI.Domain.Interfaces;

namespace WebAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IOptions<AppSettings> _settings { get; }
        public IService<User> _service { get; set; }

        public UserController(IOptions<AppSettings> settings, IService<User> service)
        {
            this._settings = settings;
            this._service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                var _userCreate = await _service.Post<UserValidator>(user);
                return Ok(_userCreate);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                var _userUpdate = await _service.Put<UserValidator>(user);
                return Ok(_userUpdate);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var _users = await _service.GetAll();
                return Ok(_users);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var _user = await _service.Get(id);
                return Ok(_user);
            }
            catch (Exception ex)
            { 
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("getAdressByCEP/{cep}")]
        public async Task<IActionResult> getAdressByCEP(string cep)
        {
            try {

                var _adress = await new AdressService().getAdressByCEP(cep);
                return Ok(_adress);
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}