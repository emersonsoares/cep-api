using CorreiosRestful.API.Helpers;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiCache;

namespace CorreiosRestful.API.Controllers
{
    public class CepController : ApiController
    {
        [OutputCacheWebApi(86400)]
        public HttpResponseMessage Get(int cep)
        {
            var requisicao = WebRequest.Create(ConfigurationManager.AppSettings["UriCorreios"]);
            requisicao.ContentType = "application/x-www-form-urlencoded";
            requisicao.Headers.Set(HttpRequestHeader.ContentEncoding, "iso-8859-1");
            requisicao.Method = "POST";

            var parser = new HTMLEnderecoParser(requisicao, cep);

            if (parser.EhValido)
                return Request.CreateResponse(HttpStatusCode.OK, parser.Endereco);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "CEP não encontrado!");
        }


    }
}