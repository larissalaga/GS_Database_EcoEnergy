using Microsoft.AspNetCore.Mvc;
using WebApplicationOdontoPrev.ViewModels;
using System;
using EcoEnergy.Data;
using EcoEnergy.Repositories.Interfaces;
using EcoEnergy.Dtos;
using EcoEnergy.Models;

namespace WebApplicationOdontoPrev.Controllers
{
    public class SimulacaoController : Controller
    {
        private readonly DataContext _context;
        private readonly ISimulacaoRepository _simulacaoRepository;
        public SimulacaoController(
            DataContext context,
            ISimulacaoRepository simulacaoRepository
            )
        {
            _context = context;
            _simulacaoRepository = simulacaoRepository;
        }
        private void SalvarSimulacao(int? id, SimulacaoViewModel model)
        {
            var simulacao = new SimulacaoDtos
            {
                NrConsumoMensal = model.NrConsumoMensal,
                NrAreaPlaca = model.NrAreaPlaca,
                DsOrcamentoSolicitado = model.DsOrcamentoSolicitado,
                NrCustoEstimado = model.NrCustoEstimado,
                NrEconomia = model.NrEconomia,
                DtSimulacao = model.DtSimulacao,
                NrPotenciaEstimada = model.NrPotenciaEstimada,
                NrProducaoMensal = model.NrProducaoMensal,
                NrTempoRetornoInvestimento = model.NrTempoRetornoInvestimento,
                IdUsuario = id.Value
            };            
            _simulacaoRepository.Create(simulacao);            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SimulacaoViewModel());
        }

        [HttpPost]
        public IActionResult MostraSimulacao(SimulacaoViewModel model)
        {   
            if (ModelState.IsValid)
            {
                // Constantes de mercado
                double precoPorKW = 5000; // Preço aproximado por kW instalado
                double eficienciaPlaca = 0.8; // 80% de eficiência
                double potenciaPorM2 = 0.15; // 150 W por m²
                double producaoMensalPorKW = 150; // Produção estimada de 150 kWh por kW por mês

                // Cálculo da potência necessária
                model.NrPotenciaEstimada = Math.Round(model.NrConsumoMensal / (eficienciaPlaca * producaoMensalPorKW), 2);

                // Cálculo da área de placas necessária
                model.NrAreaPlaca = Math.Round(model.NrPotenciaEstimada / potenciaPorM2, 2);

                // Cálculo da produção mensal estimada
                model.NrProducaoMensal = Math.Round(model.NrPotenciaEstimada * producaoMensalPorKW, 2);

                // Cálculo do custo estimado de instalação
                model.NrCustoEstimado = Math.Round(model.NrPotenciaEstimada * precoPorKW, 2);

                // Cálculo da economia mensal
                model.NrEconomia = Math.Round(model.NrConsumoMensal * model.CustoKWh, 2);

                // Cálculo do tempo de retorno do investimento
                model.NrTempoRetornoInvestimento = Math.Round(model.NrCustoEstimado / model.NrEconomia, 2);

                // Data da simulação
                model.DtSimulacao = DateTime.Now;

                int? userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    // Se o ID não estiver na sessão, redirecione para o login
                    return RedirectToAction("Index", "Login");
                }

                SalvarSimulacao(userId, model);
                return View("Index",model);
            }

            return View("Index", new SimulacaoViewModel());
        }

        [HttpPost]
        public IActionResult SolicitarOrcamento(SimulacaoViewModel model)
        {
            // Marca o orçamento como solicitado
            model.DsOrcamentoSolicitado = 1;

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                // Se o ID não estiver na sessão, redirecione para o login
                return RedirectToAction("Index", "Login");
            }

            SalvarSimulacao(userId, model);
            // Redireciona para o método Index, preservando os dados do model
            return View("Index", model);
        }
    }
}
