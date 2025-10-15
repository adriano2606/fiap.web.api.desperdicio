namespace Fiap.Web.Api.Desperdicio.Models
{
    public class Alimento
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public decimal QuantidadeKg { get; set; }
        public DateTime DataValidade { get; set; }
        public int Doado { get; set; }

    }

}
