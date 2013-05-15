using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using AddressApi.Base;
using CsQuery;

namespace AddressApi.Correios
{
    public class CorreiosMobileCrawler
    {
        private readonly WebRequest _request;

        private readonly int _zipCode;

        public CorreiosMobileCrawler(int zipCode)
        {
            _zipCode = zipCode;
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
                    using (var reader = new StreamReader(responseStream, Encoding.GetEncoding("ISO-8859-1")))
                        return reader.ReadToEnd();
                throw new EndOfStreamException();
            }
        }

        public Address ParseDocument()
        {
            var document = CQ.Create(GetPageContent());
            if (document.Select(".erro").Length > 0)
                throw new InvalidDataException();

            var div = document.Select(".respostadestaque");

            var typeOfStreet = div.Eq(0).Contents().ToString().Trim().Split(' ')[0];

            var streetNode = div.Eq(0).Contents().ToString().Trim().Split(' ');

            var street = string.Empty;
            for (var i = 0; i < streetNode.Length; i++)
            {
                if (i <= 0) continue;
                if (streetNode[i] == "-") break;
                street += streetNode[i];
                street += " ";
            }
            street = street.Trim();

            var neighborHood = div.Eq(1).Contents().ToString().Trim();
            var city = div.Eq(2).Contents().ToString().Trim().Split('/')[0].Trim();
            var estate = div.Eq(2).Contents().ToString().Trim().Split('/')[1].Trim();

            var address = new Address(_zipCode, typeOfStreet, street, neighborHood, city, estate);

            return address;
        }
    }
}
