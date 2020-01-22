using System;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces;
using RestSharp;
using System.Threading.Tasks;

namespace WebAPI.CrossCutting
{
    public class AdressCrossCutting : IAdress
    {
        public async Task<Adress> getAdressByCEP(string cep)
        {
            Adress adress = null;

            try
            {
                RestClient client = new RestClient("https://viacep.com.br/");
                RestRequest request = new RestRequest($"ws/{cep}/json", Method.GET);

                IRestResponse<Adress> response = await client.ExecuteAsync<Adress>(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    adress = response.Data;
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }

            return adress;
        }
    }
}
