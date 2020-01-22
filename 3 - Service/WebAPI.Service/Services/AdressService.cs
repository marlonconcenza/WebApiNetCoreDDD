using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;
using WebAPI.CrossCutting;
using System.Threading.Tasks;

namespace WebAPI.Service.Services
{
    public class AdressService : IAdress
    {
        public async Task<Adress> getAdressByCEP(string cep)
        {
            return await new AdressCrossCutting().getAdressByCEP(cep);
        }
    }
}