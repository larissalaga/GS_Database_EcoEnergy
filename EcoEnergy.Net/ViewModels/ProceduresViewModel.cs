using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy.ViewModels
{
    public class ProceduresViewModel
    {
        // Empresa
        public int? IdEmpresa { get; set; }
        public string NrCnpj { get; set; } = string.Empty;
        public string NmEmpresa { get; set; } = string.Empty;
        public string NrTelefone { get; set; } = string.Empty;
        public string DsEspecialidade { get; set; } = string.Empty;

        // Usuario
        public int? IdUsuario { get; set; }
        public string NmUsuario { get; set; } = string.Empty;
        public string DsEmail { get; set; } = string.Empty;
        public string NrCpf { get; set; } = string.Empty;
        public string NrTelefoneU { get; set; } = string.Empty;

        // Endereco
        public int? IdEndereco { get; set; }
        public string DsCep { get; set; } = string.Empty;
        public string DsLogradouro { get; set; } = string.Empty;
        public int NrLogradouro { get; set; }
        public string DsBairro { get; set; } = string.Empty;
        public string DsCidade { get; set; } = string.Empty;
        public string DsEstado { get; set; } = string.Empty;
        public string DsPais { get; set; } = string.Empty;

        // Orcamento
        public int? IdOrcamento { get; set; }
        public decimal NrValorProposto { get; set; }
        public string DsPrazo { get; set; } = string.Empty;
        public DateTime DtOrcamento { get; set; }
        public string DsServicos { get; set; } = string.Empty;

        // Simulacao
        public int? IdSimulacao { get; set; }
        public decimal NrCustoEstimado { get; set; }
        public decimal NrEconomia { get; set; }
        public DateTime DtSimulacao { get; set; }
        public int NrConsumoMensal { get; set; }
        public int NrAreaPlaca { get; set; }
        public int NrPotenciaEstimada { get; set; }
        public int NrProducaoMensal { get; set; }
        public int NrTempoRetornoInvestimento { get; set; }
        public int DsOrcamentoSolicitado { get; set; }

        // Listas de empresas, usuários e endereços para os dropdowns
        public List<SelectListItem> Empresas { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Usuarios { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Enderecos { get; set; } = new List<SelectListItem>();
    }
}
