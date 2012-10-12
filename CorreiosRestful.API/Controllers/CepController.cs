using CorreiosRestful.API.ViewModels;
using CsQuery;
using System.IO;
using System.Net;
using System.Web.Http;

namespace CorreiosRestful.API.Controllers
{
    public class CepController : ApiController
    {
        private CQ html;
        private WebRequest requisicao;
        private ObjetoRetorno retorno;

        public bool CepExiste()
        {
            var resposta = requisicao.GetResponse();
            return resposta.ContentLength != -1;
        }

        public ObjetoRetorno Get(string cep)
        {
            CriaRequisicao();
            ExecutaRequisicao(cep);

            var conteudo = RetornaHtml();

            if (ValidaBusca(conteudo))
                ProcessaHtml(conteudo);
            else
                retorno = new ObjetoRetorno { Mensagem = "CEP não encontrado" };

            return retorno;
        }

        public ObjetoRetorno Get()
        {
            return new ObjetoRetorno { Mensagem = "Por favor informe um CEP para fazer a busca", TemErro = true };
        }

        private void CriaRequisicao()
        {
            requisicao = WebRequest.Create("http://m.correios.com.br/movel/buscaCepConfirma.do");
            requisicao.ContentType = "application/x-www-form-urlencoded";
            requisicao.Headers.Set(HttpRequestHeader.ContentEncoding, "iso-8859-1");
            requisicao.Method = "POST";
        }

        private void ExecutaRequisicao(string cep)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(string.Format("cepEntrada={0}&tipoCep=&cepTemp&metodo=buscarCep", cep));
            requisicao.ContentLength = bytes.Length;
            var os = requisicao.GetRequestStream();

            os.Write(bytes, 0, bytes.Length);
            os.Close();
        }

        private void ProcessaHtml(string conteudo)
        {
            html = CQ.Create(conteudo).Select(".respostadestaque");

            var endereco = new Endereco
                               {
                                   TipoDeLogradouro = RetornaTipoDeLogradouro(),
                                   Logradouro = RetornaLogradouro(),
                                   Bairro = RetornaBairro(),
                                   Cidade = RetornaCidade(),
                                   Estado = RetornaEstado(),
                                   Cep = RetornaCep()
                               };

            retorno = new ObjetoRetorno { Endereco = endereco, TemErro = false, Mensagem = "CEP encontrado com sucesso" };
        }

        private string RetornaBairro()
        {
            return html.Eq(1).Contents().ToHtmlString().Trim();
        }

        private string RetornaCep()
        {
            return html.Eq(3).Contents().ToHtmlString().Trim();
        }

        private string RetornaCidade()
        {
            return html.Eq(2).Contents().ToHtmlString().Trim().Split('/')[0].Trim();
        }

        private string RetornaEstado()
        {
            return html.Eq(2).Contents().ToHtmlString().Trim().Split('/')[1].Trim();
        }

        private string RetornaHtml()
        {
            var resposta = requisicao.GetResponse();
            var responseStream = resposta.GetResponseStream();

            var reader = new StreamReader(responseStream);

            return reader.ReadToEnd();
        }

        private string RetornaLogradouro()
        {
            var logradouro = html.Eq(0).Contents().ToHtmlString().Trim().Split(' ');

            var logradouroCompleto = string.Empty;
            for (var i = 0; i < logradouro.Length; i++)
            {
                if (i <= 0) continue;
                if (logradouro[i] == "-") break;
                logradouroCompleto += logradouro[i];
                logradouroCompleto += " ";
            }
            logradouroCompleto.Trim();
            return logradouroCompleto;
        }

        private string RetornaTipoDeLogradouro()
        {
            return html.Eq(0).Contents().ToHtmlString().Trim().Split(' ')[0];
        }

        private bool ValidaBusca(string conteudo)
        {
            html = CQ.Create(conteudo).Select(".erro");
            return html.Length == 0;
        }
    }
}