using CsQuery;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace CorreiosRestful.API.Controllers
{
    public class HomeController : Controller
    {
        private string _cep;

        public HomeController()
        {
            _cep = "69906458";
        }

        public ActionResult Index()
        {
            var request = WebRequest.Create("http://m.correios.com.br/movel/buscaCepConfirma.do");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Set(HttpRequestHeader.ContentEncoding, "iso-8859-1");
            request.Method = "POST";

            WriteToUrl(request, _cep);

            var content = RetrieveFromUrl(request);
            content = ProcessHtml(content);

            ViewBag.Conteudo = content;
            return View();
        }

        private string ProcessHtml(string content)
        {
            var html = CQ.Create(content).Select(".respostadestaque");

            var tipoDeLogradouro = RetornaTipoDeLogradouro(html);
            var logradouro = RetornaLogradouro(html);
            var bairro = RetornaBairro(html);
            var cidade = RetornaCidade(html);
            var estado = RetornaEstado(html);

            return estado;
        }

        private string RetornaBairro(CQ html)
        {
            return html.Eq(1).Contents().ToHtmlString().Trim();
        }

        private string RetornaCidade(CQ html)
        {
            return html.Eq(2).Contents().ToHtmlString().Trim().Split('/')[0].Trim();
        }

        private string RetornaEstado(CQ html)
        {
            return html.Eq(2).Contents().ToHtmlString().Trim().Split('/')[1].Trim();
        }

        private string RetornaLogradouro(CQ html)
        {
            var logradouro = html.Eq(0).Contents().ToHtmlString().Trim().Split(' ');

            var logradouroCompleto = string.Empty;
            for (var i = 0; i < logradouro.Length; i++)
            {
                if (i <= 0 || logradouro[i] == "-") continue;
                logradouroCompleto += logradouro[i];
                logradouroCompleto += " ";
            }
            logradouroCompleto.Trim();
            return logradouroCompleto;
        }

        private string RetornaTipoDeLogradouro(CQ html)
        {
            return html.Eq(0).Contents().ToHtmlString().Trim().Split(' ')[0];
        }

        private string RetrieveFromUrl(WebRequest request)
        {
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            var reader = new StreamReader(responseStream);

            return reader.ReadToEnd();
        }

        private void WriteToUrl(WebRequest request, string cep)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(string.Format("cepEntrada={0}&tipoCep=&cepTemp&metodo=buscarCep", cep));
            request.ContentLength = bytes.Length;
            var os = request.GetRequestStream();

            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
        }
    }
}