using EcoEnergy.Models;
using EcoEnergy.Dtos;

namespace EcoEnergy.Repositories.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Endereco> Create(EnderecoDtos endereco);
        Task<bool> DeleteById(int id);
        Task<Endereco> GetById(int id);
        Task<List<Endereco>> GetAll();
        Task<List<Endereco>> GetByUsuario(int id_usuario);
        Task<List<Endereco>> GetByEmpresa(int id_empresa);
        Task<Endereco> UpdateById(int id, EnderecoDtos endereco);
    }
}
