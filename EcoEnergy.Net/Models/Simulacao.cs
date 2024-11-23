using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy.Models
{
    [Table("T_GS24_SIMULACAO")]
    public class Simulacao
    {
        [Key]
        [Required]
        [Column("ID_SIMULACAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSimulacao { get; set; }

        [Required]
        [Column("NR_CUSTO_ESTIMADO")]
        public double NrCustoEstimado { get; set; } = 0;

        [Required]
        [Column("NR_ECONOMIA")]
        public double NrEconomia { get; set; } = 0;

        [Required]
        [Column("DT_SIMULACAO")]
        [DataType(DataType.DateTime)]
        public DateTime DtSimulacao { get; set; } = new DateTime();

        [Required]
        [Column("NR_CONSUMO_MENSAL")]
        public double NrConsumoMensal { get; set; } = 0;

        [Required]
        [Column("NR_AREA_PLACA")]
        public double NrAreaPlaca { get; set; } = 0;

        [Required]
        [Column("NR_POTENCIA_ESTIMADA")]
        public double NrPotenciaEstimada { get; set; } = 0;

        [Required]
        [Column("NR_PRODUCAO_MENSAL")]
        public double NrProducaoMensal { get; set; } = 0;

        [Required]
        [Column("NR_TEMPO_RETORNO_INVESTIMENTO")]
        public double NrTempoRetornoInvestimento { get; set; } = 0;

        [Required]
        [Column("DS_ORCAMENTO_SOLICITADO")]
        public int DsOrcamentoSolicitado { get; set; } = 0;

        [Required]
        [Column("ID_USUARIO")]
        [ForeignKey("USUARIO")]
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        [Required]
        [Column("ID_ENDERECO")]
        [ForeignKey("ENDERECO")]
        public int IdEndereco { get; set; }
        public Endereco? Endereco { get; set; }
    }
}
