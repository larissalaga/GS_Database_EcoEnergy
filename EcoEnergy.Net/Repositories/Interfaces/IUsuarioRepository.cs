using EcoEnergy.Dtos;

namespace EcoEnergy.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Models.Usuario> Create(UsuarioDtos usuario);
        Task<bool> DeleteById(int id);
        public void Delete(string nr_cpf);
        Task<List<Models.Usuario>> GetAll();
        Task<Models.Usuario> GetById(int id);
        Task<Models.Usuario> GetByCPF(string nr_cpf);
        Task<Models.Usuario> Update(int id, UsuarioDtos usuario);
        Task<Models.Usuario> GetByEmail(string ds_email);
    }
}
