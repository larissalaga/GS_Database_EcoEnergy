using EcoEnergy.Models;
using EcoEnergy.Dtos;

namespace EcoEnergy.Repositories.Interfaces
{
    public interface IOrcamentoRepository
    {
        Task<Orcamento> Create(OrcamentoDtos orcamento);
        Task<bool> DeleteById(int id);
        Task<Orcamento> GetById(int id);
        Task<List<Orcamento>> GetAll();
        Task<List<Orcamento>> GetByEmpresa(int id_empresa);
        Task<List<Orcamento>> GetBySimulacao(int id_simulacao);
        Task<Orcamento> Update(OrcamentoDtos orcamento);
    }
}
