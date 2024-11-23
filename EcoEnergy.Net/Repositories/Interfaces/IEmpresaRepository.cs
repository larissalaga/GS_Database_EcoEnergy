using EcoEnergy.Models;
using EcoEnergy.Dtos;

namespace EcoEnergy.Repositories.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<Empresa> Create(EmpresaDtos empresa);
        Task<bool> DeleteById(int id);
        public void Delete(string nr_cnpj);
        Task<List<Empresa>> GetAll();
        Task<Empresa> GetById(int id);
        Task<List<Empresa>> GetByCnpj(string nr_cnpj);
        Task<Empresa> Update(int id, EmpresaDtos empresa);

    }
}
