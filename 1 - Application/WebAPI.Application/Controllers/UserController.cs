using System;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Service.Services;
using WebAPI.Service.Validators;
using WebAPI.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WebAPI.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IOptions<AppSettings> settings;
        private BaseService<User> service = null;

        public UserController(IOptions<AppSettings> settings)
        {
            this.settings = settings;
            this.service = new BaseService<User>(this.settings);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            Response _response = await service.Post<UserValidator>(user);
            return Ok(_response);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] User user)
        {
            try
            {
                await service.Put<UserValidator>(user);

                return new ObjectResult(user);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex);
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
                await service.Delete(id);

                return new NoContentResult();
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex);
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
                return new ObjectResult(await service.GetAll());
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
                return new ObjectResult(await service.Get(id));
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex);
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

                Adress adress = await new AdressService().getAdressByCEP(cep);
                return Ok(adress);
            }
            catch(Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}