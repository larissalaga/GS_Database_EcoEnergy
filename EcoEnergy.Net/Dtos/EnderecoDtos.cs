using EcoEnergy.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy.Dtos
{
    public class EnderecoDtos
    {
        [Required]
        [MaxLength(8)]
        public string DsCep { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DsLogradouro { get; set; } = string.Empty;

        [Required]
        public int NrLogradouro { get; set; } = 0;

        [Required]
        [MaxLength(100)]
        public string DsBairro { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DsCidade { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DsEstado { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DsPais { get; set; } = string.Empty;

        [Column("id_usuario")]
        [ForeignKey("Usuario")]
        public int? IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }

        [Column("id_empresa")]
        [ForeignKey("Empresa")]
        public int? IdEmpresa { get; set; }
        public Empresa? Empresa { get; set; }
    }
}