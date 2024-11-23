using EcoEnergy.Data;
using EcoEnergy.Dtos;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy.Repositories.Implementations
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private DataContext _context;

        public OrcamentoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Models.Orcamento> Create(OrcamentoDtos orcamento)
        {
            var getOrcamento = await _context.Orcamento.FirstOrDefaultAsync(x => x.IdSimulacao == orcamento.IdSimulacao);
            if (getOrcamento != null)
            {
                throw new Exception("Orçamento já cadastrado");
            }
            else
            {
                var newOrcamento = new Models.Orcamento
                {
                    NrValorProposto = orcamento.NrValorProposto,
                    DsPrazo = orcamento.DsPrazo,
                    DtOrcamento = orcamento.DtOrcamento,
                    DsServicos = orcamento.DsServicos,
                    IdSimulacao = orcamento.IdSimulacao,
                    IdEmpresa = orcamento.IdEmpresa
                };
                _context.Orcamento.Add(newOrcamento);
                await _context.SaveChangesAsync();
                return newOrcamento;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            var getOrcamento = await _context.Orcamento.FirstOrDefaultAsync(x => x.IdOrcamento == id);
            if (getOrcamento == null)
            {
                throw new Exception("Orçamento não encontrado");
            }
            else
            {
                _context.Orcamento.Remove(getOrcamento);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Models.Orcamento> GetById(int id)
        {
            var getOrcamento = await _context.Orcamento.FirstOrDefaultAsync(x => x.IdOrcamento == id);
            if (getOrcamento == null)
            {
                throw new Exception("Orçamento não encontrado");
            }
            else
            {
                return getOrcamento;
            }
        }

        public async Task<List<Models.Orcamento>> GetAll()
        {
            var getOrcamento = await _context.Orcamento.ToListAsync();
            if (getOrcamento == null)
            {
                throw new Exception("Nenhum orçamento encontrado");
            }
            else
            {
                return getOrcamento;
            }
        }

        public async Task<List<Models.Orcamento>> GetByEmpresa(int id_empresa)
        {
            var getOrcamento = await _context.Orcamento.Where(x => x.IdEmpresa == id_empresa).ToListAsync();
            if (getOrcamento == null)
            {
                throw new Exception("Nenhum orçamento encontrado");
            }
            else
            {
                return getOrcamento;
            }
        }

        public async Task<List<Models.Orcamento>> GetBySimulacao(int id_simulacao)
        {
            var getOrcamento = await _context.Orcamento.Where(x => x.IdSimulacao == id_simulacao).ToListAsync();
            if (getOrcamento == null)
            {
                throw new Exception("Nenhum orçamento encontrado");
            }
            else
            {
                return getOrcamento;
            }
        }

        public async Task<Models.Orcamento> Update(OrcamentoDtos orcamento)
        {
            var getOrcamento = await _context.Orcamento.FirstOrDefaultAsync(x => x.IdSimulacao == orcamento.IdSimulacao);
            if (getOrcamento == null)
            {
                throw new Exception("Orçamento não encontrado");
            }
            else
            {
                getOrcamento.NrValorProposto = orcamento.NrValorProposto;
                getOrcamento.DsPrazo = orcamento.DsPrazo;
                getOrcamento.DtOrcamento = orcamento.DtOrcamento;
                getOrcamento.DsServicos = orcamento.DsServicos;
                getOrcamento.IdSimulacao = orcamento.IdSimulacao;
                getOrcamento.IdEmpresa = orcamento.IdEmpresa;
                await _context.SaveChangesAsync();
                return getOrcamento;
            }
        }
    }
}
