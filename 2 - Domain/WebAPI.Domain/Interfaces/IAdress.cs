using System.Threading.Tasks;
using WebAPI.Domain.Entities;

namespace WebAPI.Domain.Interfaces
{
    public interface IAdress
    {
         Task<Adress> getAdressByCEP(string cep);
    }
}