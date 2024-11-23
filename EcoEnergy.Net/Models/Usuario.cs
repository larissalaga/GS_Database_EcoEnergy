using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy.Models
{
    [Table("T_GS24_USUARIO")]
    public class Usuario
    {
        [Key]
        [Required]
        [Column("ID_USUARIO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        [Column("NM_USUARIO")]
        [MaxLength(70)]
        public string NmUsuario { get; set; } = string.Empty;

        [Required]
        [Column("DS_EMAIL")]
        [MaxLength(100)]
        public string DsEmail { get; set; } = string.Empty;

        [Required]
        [Column("NR_TELEFONE")]
        [MaxLength(13)]
        public string NrTelefone { get; set; } = string.Empty;

        [Required]
        [Column("NR_CPF")]
        [MaxLength(11)]
        public string NrCpf { get; set; } = string.Empty;

        [Column("ID_EMPRESA")]
        [ForeignKey("EMPRESA")]
        public int? IdEmpresa { get; set; }
        public Empresa? Empresa { get; set; }
    }
}
