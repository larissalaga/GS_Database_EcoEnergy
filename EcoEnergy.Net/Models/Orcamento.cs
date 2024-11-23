using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy.Models
{
    [Table("T_GS24_ORCAMENTO")]
    public class Orcamento
    {
        [Key]
        [Required]
        [Column("ID_ORCAMENTO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrcamento { get; set; }

        [Required]
        [Column("NR_VALOR_PROPOSTO")]
        public double NrValorProposto { get; set; }

        [Required]
        [Column("DS_PRAZO")]
        [MaxLength(20)]
        public string DsPrazo { get; set; } = string.Empty;

        [Required]
        [Column("DT_ORCAMENTO")]
        [DataType(DataType.DateTime)]
        public DateTime DtOrcamento { get; set; } = new DateTime();

        [Required]
        [Column("DS_SERVICOS")]
        [MaxLength(200)]
        public string DsServicos { get; set; } = string.Empty;

        [Required]
        [Column("ID_SIMULACAO")]
        [ForeignKey("SIMULACAO")]
        public int IdSimulacao { get; set; }
        public Simulacao? Simulacao { get; set; }

        [Required]
        [Column("ID_EMPRESA")]
        [ForeignKey("EMPRESA")]
        public int IdEmpresa { get; set; }
        public Empresa? Empresa { get; set; }
    }
}
