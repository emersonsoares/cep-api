using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AddressApi.Correios;

namespace AddressApi.RestApi.Controllers
{
    public class AddressController : ApiController
    {
        public HttpResponseMessage Get(string zipCode)
        {
            var repository = new CorreiosRepository();
            try
            {
                var address = repository.GetAddress(zipCode);

                var addressDTO = new
                {
                    Cep = address.ZipCode,
                    TipoDeLogradouro = address.TypeOfStreet,
                    Logradouro = address.Street,
                    Bairro = address.Neighborhood,
                    Cidade = address.City,
                    Estado = address.Estate
                };
                return Request.CreateResponse(HttpStatusCode.OK, addressDTO);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Endereço não encontrado!");
            }
        }
    }
}
