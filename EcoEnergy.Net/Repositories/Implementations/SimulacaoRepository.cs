using EcoEnergy.Data;
using EcoEnergy.Dtos;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy.Repositories.Implementations
{
    public class SimulacaoRepository : ISimulacaoRepository
    {
        private DataContext _context;
        public SimulacaoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Simulacao> Create(SimulacaoDtos simulacao)
        {
            var newSimulacao = new Models.Simulacao
                {
                    NrCustoEstimado = simulacao.NrCustoEstimado,
                    NrEconomia = simulacao.NrEconomia,
                    DtSimulacao = simulacao.DtSimulacao,
                    NrConsumoMensal = simulacao.NrConsumoMensal,
                    NrAreaPlaca = simulacao.NrAreaPlaca,
                    NrPotenciaEstimada = simulacao.NrPotenciaEstimada,
                    NrProducaoMensal = simulacao.NrProducaoMensal,
                    NrTempoRetornoInvestimento = simulacao.NrTempoRetornoInvestimento,
                    DsOrcamentoSolicitado = simulacao.DsOrcamentoSolicitado,
                    IdUsuario = simulacao.IdUsuario,
                    IdEndereco = simulacao.IdEndereco
                };
                _context.Simulacao.Add(newSimulacao);
                await _context.SaveChangesAsync();
                return newSimulacao;
        }
        public async Task<bool> DeleteById(int id)
        {
            var getSimulacao = await _context.Simulacao.FirstOrDefaultAsync(x => x.IdSimulacao == id);
            if (getSimulacao == null)
            {
                throw new Exception("Simulação não encontrada");
            }
            else
            {
                _context.Simulacao.Remove(getSimulacao);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<Models.Simulacao> GetById(int id)
        {
            var getSimulacao = await _context.Simulacao.FirstOrDefaultAsync(x => x.IdSimulacao == id);
            if (getSimulacao == null)
            {
                throw new Exception("Simulação não encontrada");
            }
            else
            {
                return getSimulacao;
            }
        }

        public async Task<List<Models.Simulacao>> GetAll()
        {
            var getSimulacao = await _context.Simulacao.ToListAsync();
            if (getSimulacao == null)
            {
                throw new Exception("Simulação não encontrada");
            }
            else
            {
                return getSimulacao;
            }
        }

        public async Task<List<Models.Simulacao>> GetByUsuario(int id_usuario)
        {
            var getSimulacao = await _context.Simulacao.Where(x => x.IdUsuario == id_usuario).ToListAsync();
            if (getSimulacao == null)
            {
                throw new Exception("Simulação não encontrada");
            }
            else
            {
                return getSimulacao;
            }
        }

        public async Task<Models.Simulacao> Update(int id, SimulacaoDtos simulacao)
        {
            var getSimulacao = await _context.Simulacao.FirstOrDefaultAsync(x => x.IdSimulacao == id);
            if (getSimulacao == null)
            {
                throw new Exception("Simulação não encontrada");
            }
            else
            {
                getSimulacao.NrCustoEstimado = simulacao.NrCustoEstimado;
                getSimulacao.NrEconomia = simulacao.NrEconomia;
                getSimulacao.DtSimulacao = simulacao.DtSimulacao;
                getSimulacao.NrConsumoMensal = simulacao.NrConsumoMensal;
                getSimulacao.NrAreaPlaca = simulacao.NrAreaPlaca;
                getSimulacao.NrPotenciaEstimada = simulacao.NrPotenciaEstimada;
                getSimulacao.NrProducaoMensal = simulacao.NrProducaoMensal;
                getSimulacao.NrTempoRetornoInvestimento = simulacao.NrTempoRetornoInvestimento;
                getSimulacao.DsOrcamentoSolicitado = simulacao.DsOrcamentoSolicitado;
                getSimulacao.IdUsuario = simulacao.IdUsuario;
                getSimulacao.IdEndereco = simulacao.IdEndereco;
                await _context.SaveChangesAsync();
                return getSimulacao;
            }
        }
    }
}
