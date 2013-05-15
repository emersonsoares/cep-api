using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AddressApi.Correios;

namespace AddressApi.RestApi.Controllers
{
    public class AddressController : ApiController
    {
        public HttpResponseMessage Get(int zipCode)
        {
            var repository = new CorreiosRepository();

            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, repository.GetAddress(zipCode));
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Endereço não encontado!");
            }
        }
    }
}
