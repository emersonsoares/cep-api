namespace CorreiosRestful.API.ViewModels
{
    public class ObjetoRetorno
    {
        public ObjetoRetorno()
        {
            TemErro = true;
        }

        public Endereco Endereco { get; set; }

        public string Mensagem { get; set; }

        public bool TemErro { get; set; }
    }
}