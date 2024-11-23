using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy.Models
{
    [Table("T_GS24_ENDERECO")]
    public class Endereco
    {
        [Key]
        [Required]
        [Column("ID_ENDERECO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEndereco { get; set; }

        [Required]
        [Column("DS_CEP")]
        [MaxLength(8)]
        public string DsCep { get; set; } = string.Empty;

        [Required]
        [Column("DS_LOGRADOURO")]
        [MaxLength(100)]
        public string DsLogradouro { get; set; } = string.Empty;

        [Required]
        [Column("NR_LOGRADOURO")]
        public int NrLogradouro { get; set; }

        [Required]
        [Column("DS_BAIRRO")]
        [MaxLength(100)]
        public string DsBairro { get; set; } = string.Empty;

        [Required]
        [Column("DS_CIDADE")]
        [MaxLength(100)]
        public string DsCidade { get; set; } = string.Empty;

        [Required]
        [Column("DS_ESTADO")]
        [MaxLength(100)]
        public string DsEstado { get; set; } = string.Empty;

        [Required]
        [Column("DS_PAIS")]
        [MaxLength(100)]
        public string DsPais { get; set; } = string.Empty;

        [Column("ID_USUARIO")]
        [ForeignKey("USUARIO")]
        public int? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        [Column("ID_EMPRESA")]
        [ForeignKey("EMPRESA")]
        public int? IdEmpresa { get; set; }
        public Empresa? Empresa { get; set; }
    }
}
