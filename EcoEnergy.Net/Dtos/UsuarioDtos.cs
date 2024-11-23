using EcoEnergy.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy.Dtos
{
    public class UsuarioDtos
    {
        [Required]
        [MaxLength(70)]
        public string NmUsuario { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DsEmail { get; set; } = string.Empty;

        [Required]
        [MaxLength(13)]
        public string NrTelefone { get; set; } = string.Empty;

        [Required]
        [MaxLength(11)]
        public string NrCpf { get; set; } = string.Empty;

        [Column("id_empresa")]
        [ForeignKey("Empresa")]
        public int IdEmpresa { get; set; }
        public Empresa? Empresa { get; set; }
    }
}
