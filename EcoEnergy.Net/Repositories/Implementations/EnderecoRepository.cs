using EcoEnergy.Data;
using EcoEnergy.Dtos;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy.Repositories.Implementations
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private DataContext _context;
        public EnderecoRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Endereco> Create(EnderecoDtos endereco)
        {
            var getEndereco = await _context.Endereco.FirstOrDefaultAsync(x => x.DsCep == endereco.DsCep);
            if (getEndereco != null)
            {
                throw new Exception("Endereço já cadastrado");
            }
            else
            {
                var newEndereco = new Models.Endereco
                {
                    DsCep = endereco.DsCep,
                    DsLogradouro = endereco.DsLogradouro,
                    NrLogradouro = endereco.NrLogradouro,
                    DsBairro = endereco.DsBairro,
                    DsCidade = endereco.DsCidade,
                    DsEstado = endereco.DsEstado,
                    DsPais = endereco.DsPais,
                    IdUsuario = endereco.IdUsuario,
                    IdEmpresa = endereco.IdEmpresa
                };
                _context.Endereco.Add(newEndereco);
                await _context.SaveChangesAsync();
                return newEndereco;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            var getEndereco = await _context.Endereco.FirstOrDefaultAsync(x => x.IdEndereco == id);
            if (getEndereco == null)
            {
                throw new Exception("Endereço não encontrado");
            }
            else
            {
                _context.Endereco.Remove(getEndereco);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Models.Endereco> GetById(int id)
        {
            var getEndereco = await _context.Endereco.FirstOrDefaultAsync(x => x.IdEndereco == id);
            if (getEndereco == null)
            {
                throw new Exception("Endereço não encontrado");
            }
            else
            {
                return getEndereco;
            }
        }

        public async Task<List<Models.Endereco>> GetAll()
        {
            var getEndereco = await _context.Endereco.ToListAsync();
            if (getEndereco == null)
            {
                throw new Exception("Nenhum endereço encontrado");
            }
            else
            {
                return getEndereco;
            }
        }

        public async Task<List<Models.Endereco>> GetByUsuario(int id_usuario)
        {
            var getEndereco = await _context.Endereco.Where(x => x.IdUsuario == id_usuario).ToListAsync();
            if (getEndereco == null)
            {
                throw new Exception("Nenhum endereço encontrado");
            }
            else
            {
                return getEndereco;
            }
        }

        public async Task<List<Models.Endereco>> GetByEmpresa(int id_empresa)
        {
            var getEndereco = await _context.Endereco.Where(x => x.IdEmpresa == id_empresa).ToListAsync();
            if (getEndereco == null)
            {
                throw new Exception("Nenhum endereço encontrado");
            }
            else
            {
                return getEndereco;
            }
        }

        public async Task<Models.Endereco> UpdateById(int id, EnderecoDtos endereco)
        {
            var getEndereco = await _context.Endereco.FirstOrDefaultAsync(x => x.IdEndereco == id);
            if (getEndereco == null)
            {
                throw new Exception("Endereço não encontrado");
            }
            else
            {
                getEndereco.DsCep = endereco.DsCep;
                getEndereco.DsLogradouro = endereco.DsLogradouro;
                getEndereco.NrLogradouro = endereco.NrLogradouro;
                getEndereco.DsBairro = endereco.DsBairro;
                getEndereco.DsCidade = endereco.DsCidade;
                getEndereco.DsEstado = endereco.DsEstado;
                getEndereco.DsPais = endereco.DsPais;
                getEndereco.IdUsuario = endereco.IdUsuario;
                getEndereco.IdEmpresa = endereco.IdEmpresa;
                await _context.SaveChangesAsync();
                return getEndereco;
            }
        }
    }
}
