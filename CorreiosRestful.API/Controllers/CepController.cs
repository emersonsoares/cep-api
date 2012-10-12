using CorreiosRestful.API.ViewModels;
using System.Web.Http;

namespace CorreiosRestful.API.Controllers
{
    public class CepController : ApiController
    {
        public Endereco Get(string cep)
        {
            return new Endereco();
        }
    }
}