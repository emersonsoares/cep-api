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
			requisicao.Headers.Set(HttpRequestHeader.ContentEncoding, "utf-8");
			requisicao.Method = "POST";

			var parse = new HTMLEnderecoParse(requisicao, cep);

			if (parse.EhValido)
				return Request.CreateResponse(HttpStatusCode.OK, parse.Endereco);
			return Request.CreateErrorResponse(HttpStatusCode.NotFound, "CEP não encontrado!");
        }


    }
}