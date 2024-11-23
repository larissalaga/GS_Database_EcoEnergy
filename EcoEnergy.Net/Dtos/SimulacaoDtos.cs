using EcoEnergy.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy.Dtos
{
    public class SimulacaoDtos
    {
        [Required]
        public double NrCustoEstimado { get; set; } = 0;

        [Required]
        public double NrEconomia { get; set; } = 0;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DtSimulacao { get; set; } = new DateTime();

        [Required]
        public double NrConsumoMensal { get; set; } = 0;

        [Required]
        public double NrAreaPlaca { get; set; } = 0;

        [Required]
        public double NrPotenciaEstimada { get; set; } = 0;

        [Required]
        public double NrProducaoMensal { get; set; } = 0;

        [Required]
        public double NrTempoRetornoInvestimento { get; set; } = 0;

        [Required]
        public int DsOrcamentoSolicitado { get; set; } = 0;

        [Column("id_usuario")]
        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; } 

        [Column("id_endereco")]
        [ForeignKey("Endereco")]
        public int IdEndereco { get; set; }
        public Endereco? Endereco { get; set; } 
    }
}
