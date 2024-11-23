namespace WebApplicationOdontoPrev.ViewModels
{
    public class ConsumoViewModel
    {
        public int QuantidadeGeladeira { get; set; }
        public int QuantidadeTelevisao { get; set; }
        public int QuantidadeMicroondas { get; set; }
        public int QuantidadeArCondicionado { get; set; }
        public double ConsumoTotal { get; set; }
        public double CustoTotal { get; set; }
        public double CustoKWh { get; set; } = 0.70; // Novo campo para o custo médio do kWh
    }

}
