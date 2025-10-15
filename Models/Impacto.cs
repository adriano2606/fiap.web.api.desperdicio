namespace Fiap.Web.Api.Desperdicio.Models
{
    public class Impacto
    {
        public int? Id { get; set; }
        public int TotalKgReaproveitados { get; set; }
        public int TotalRefeicoesGeradas { get; set; }
        public double Co2EconomizadoKg { get; set; }
        public DateTime DataReferencia { get; set; }
    }

}
