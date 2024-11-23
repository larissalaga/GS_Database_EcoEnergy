using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy.Dtos
{
    public class EmpresaDtos
    {
        [Required]
        [MaxLength(14)]
        public string NrCnpj { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string NmEmpresa { get; set; } = string.Empty;

        [Required]
        [MaxLength(13)]
        public string NrTelefone { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string DsEspecialidade { get; set; } = string.Empty;
    }
}