using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace AddressApi.Correios
{
    public class CorreiosMobileCrawler
    {
        private readonly WebRequest _request;

        public CorreiosMobileCrawler(int zipCode)
        {
            _request = WebRequest.Create(ConfigurationManager.AppSettings["UriCorreios"]);
            _request.ContentType = "application/x-www-form-urlencoded";
            _request.Headers.Set(HttpRequestHeader.ContentEncoding, "ISO-8859-1");
            _request.Method = "POST";
            var requestParams = Encoding.ASCII.GetBytes(string.Format("cepEntrada={0}&tipoCep=&cepTemp&metodo=buscarCep", zipCode));
            _request.ContentLength = requestParams.Length;

            var requestStream = _request.GetRequestStream();
            requestStream.Write(requestParams, 0, requestParams.Length);
            requestStream.Close();
        }

        private string GetPageContent()
        {
            var response = _request.GetResponse();

            using (var responseStream = response.GetResponseStream())
            {
                if (responseStream != null)
                    using (var reader = new StreamReader(responseStream, Encoding.GetEncoding("IS-8859-1")))
                        return reader.ReadToEnd();
                throw new EndOfStreamException();
            }
        }


    }
}
