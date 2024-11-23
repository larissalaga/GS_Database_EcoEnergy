using System.ComponentModel.DataAnnotations;

namespace WebApplicationOdontoPrev.ViewModels
{
    public class SimulacaoViewModel
    {        
        // Entradas
        public double NrConsumoMensal { get; set; } = 0;
        public double NrAreaPlaca { get; set; } = 0; // Valor padrão de área de placa
        public int DsOrcamentoSolicitado { get; set; } = 0; // Valor padrão de orçamento solicitado
        public double CustoKWh { get; set; } = 0.70; // Valor padrão de custo do kWh

        // Calculado
        public double NrCustoEstimado { get; set; } = 0;
        public double NrEconomia { get; set; } = 0;
        public DateTime DtSimulacao { get; set; } = DateTime.Now;
        public double NrPotenciaEstimada { get; set; } = 0;
        public double NrProducaoMensal { get; set; } = 0;
        public double NrTempoRetornoInvestimento { get; set; } = 0;
    }
}