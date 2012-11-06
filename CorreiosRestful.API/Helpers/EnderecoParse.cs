using CorreiosRestful.API.ViewModels;
using CsQuery;
using System.IO;
using System.Net;

namespace CorreiosRestful.API.Helpers
{
	public class HTMLEnderecoParse
	{
		private CQ _csQueryParsed;
		public Endereco Endereco { get; private set; }

		public HTMLEnderecoParse(WebRequest requisicao, int cep)
		{
			var bytes = System.Text.Encoding.ASCII.GetBytes(string.Format("cepEntrada={0}&tipoCep=&cepTemp&metodo=buscarCep", cep));
            requisicao.ContentLength = bytes.Length;
			using (var os = requisicao.GetRequestStream())
			{
				os.Write(bytes, 0, bytes.Length);
				os.Close();
			}

			var resposta = requisicao.GetResponse();
			using (var responseStream = resposta.GetResponseStream())
			using (var reader = new StreamReader(responseStream))
			{
				var html = reader.ReadToEnd();
				_csQueryParsed = CQ.Create(html);
			}

			if (EhValido)
			{
				var htmlResp = _csQueryParsed.Select(".respostadestaque");
				Endereco = new Endereco
				{
					Bairro = htmlResp.Eq(1).Contents().ToHtmlString().Trim()
					,
					Cep = htmlResp.Eq(3).Contents().ToHtmlString().Trim()
					,
					Cidade = htmlResp.Eq(2).Contents().ToHtmlString().Trim().Split('/')[0].Trim()
					,
					Estado = htmlResp.Eq(2).Contents().ToHtmlString().Trim().Split('/')[1].Trim()
					,
					TipoDeLogradouro = htmlResp.Eq(0).Contents().ToHtmlString().Trim().Split(' ')[0]
				};
				var logradouro = htmlResp.Eq(0).Contents().ToHtmlString().Trim().Split(' ');
				var logradouroCompleto = string.Empty;
				for (var i = 0; i < logradouro.Length; i++)
				{
					if (i <= 0) continue;
					if (logradouro[i] == "-") break;
					logradouroCompleto += logradouro[i];
					logradouroCompleto += " ";
				}
				Endereco.Logradouro = logradouroCompleto.Trim();
			}
		}

		public bool EhValido
		{
			get
			{
				var html = _csQueryParsed.Select(".erro");
				return html.Length == 0;
			}
		}

	}
}