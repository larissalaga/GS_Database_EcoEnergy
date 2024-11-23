using EcoEnergy.Models;
using EcoEnergy.Dtos;

namespace EcoEnergy.Repositories.Interfaces
{
    public interface ISimulacaoRepository
    {
        Task<Simulacao> Create(SimulacaoDtos simulacao);
        Task<bool> DeleteById(int id);
        Task<Simulacao> GetById(int id);
        Task<List<Simulacao>> GetAll();
        Task<List<Simulacao>> GetByUsuario(int id_usuario);
        Task<Simulacao> Update(int id, SimulacaoDtos simulacao);
    }
}
