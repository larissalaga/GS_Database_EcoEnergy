using EcoEnergy.Data;
using EcoEnergy.Dtos;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private DataContext _context;        
        public UsuarioRepository(
            DataContext context
            )
        {
            _context = context;
        }

        public async Task<Models.Usuario> Create(UsuarioDtos usuario)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.NrCpf == usuario.NrCpf);
            if (getUsuario == null)
            {
                throw new Exception("Usuário já cadastrado");
            }
            else
            {
                var newUsuario = new Models.Usuario
                {
                    NrCpf = usuario.NrCpf,
                    NmUsuario = usuario.NmUsuario,
                    DsEmail = usuario.DsEmail,
                    NrTelefone = usuario.NrTelefone,
                    IdEmpresa = usuario.IdEmpresa
                };
                _context.Usuario.Add(newUsuario);
                await _context.SaveChangesAsync();
                return newUsuario;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
            if (getUsuario == null)
            {
                return true;
            }
            else
            {
                _context.Usuario.Remove(getUsuario);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async void Delete(string nr_cpf)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.NrCpf == nr_cpf);
            if (getUsuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            else
            {
                _context.Usuario.Remove(getUsuario);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Models.Usuario>> GetAll()
        {
            var getUsuario = await _context.Usuario.ToListAsync();
            if (getUsuario == null)
            {
                throw new Exception("Nenhum usuário cadastrado");
            }
            else
            {
                return getUsuario;
            }
        }
        public async Task<Models.Usuario> GetById(int id)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
            if (getUsuario == null)
            {
                throw new Exception("Usuário não cadastrado");
            }
            else
            {
                return getUsuario;
            }
        }

        public async Task<Models.Usuario> GetByCPF(string nr_cpf)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.NrCpf == nr_cpf);
            if (getUsuario == null)
            {
                throw new Exception("Usuário não cadastrado");
            }
            else
            {
                return getUsuario;
            }
        }
        public async Task<Models.Usuario> GetByEmail(string ds_email)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.DsEmail == ds_email);
            if (getUsuario == null)
            {
                throw new Exception("Usuário não cadastrado");
            }
            else
            {
                return getUsuario;
            }
        }
        public async Task<Models.Usuario> Update(int id, UsuarioDtos usuario)
        {
            var getUsuario = await _context.Usuario.FirstOrDefaultAsync(x => x.IdUsuario == id);
            if (getUsuario == null)
            {
                throw new Exception("Usuário não cadastrado");
            }
            else
            {
                getUsuario.NmUsuario = usuario.NmUsuario;
                getUsuario.NrCpf = usuario.NrCpf;
                getUsuario.NrTelefone = usuario.NrTelefone;
                getUsuario.DsEmail = usuario.DsEmail;
                getUsuario.IdEmpresa = usuario.IdEmpresa;                
                await _context.SaveChangesAsync();
                return getUsuario;
            }
        }

    }
}
