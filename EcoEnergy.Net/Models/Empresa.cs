using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy.Models
{
    [Table("T_GS24_EMPRESA")]
    public class Empresa
    {
        [Key]
        [Required]
        [Column("ID_EMPRESA")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpresa { get; set; }

        [Required]
        [Column("NR_CNPJ")]
        [MaxLength(14)]
        public string NrCnpj { get; set; } = string.Empty;

        [Required]
        [Column("NM_EMPRESA")]
        [MaxLength(100)]
        public string NmEmpresa { get; set; } = string.Empty;

        [Required]
        [Column("NR_TELEFONE")]
        [MaxLength(13)]
        public string NrTelefone { get; set; } = string.Empty;

        [Required]
        [Column("DS_ESPECIALIDADE")]
        [MaxLength(200)]
        public string DsEspecialidade { get; set; } = string.Empty;
    }
}
