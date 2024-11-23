using EcoEnergy.Data;
using EcoEnergy.Dtos;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoEnergy.Repositories.Implementations
{
    public class EmpresaRepository : IEmpresaRepository 
    {
        private DataContext _context;

        public EmpresaRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Models.Empresa> Create(EmpresaDtos empresa)
        {
            var getEmpresa = await _context.Empresa.FirstOrDefaultAsync(x => x.NrCnpj == empresa.NrCnpj);
            if (getEmpresa != null)
            {
                throw new Exception("Empresa já cadastrada");
            }
            else
            {
                var newEmpresa = new Models.Empresa
                {
                    NrCnpj = empresa.NrCnpj,
                    NmEmpresa = empresa.NmEmpresa,
                    NrTelefone = empresa.NrTelefone,
                    DsEspecialidade = empresa.DsEspecialidade
                };
                _context.Empresa.Add(newEmpresa);
                await _context.SaveChangesAsync();
                return newEmpresa;
            }
        }
        public async Task<bool> DeleteById(int id)
        {
            var getEmpresa = await _context.Empresa.FirstOrDefaultAsync(x => x.IdEmpresa == id);
            if (getEmpresa == null)
            {
                throw new Exception("Empresa não encontrada");
            }
            else
            {
                _context.Empresa.Remove(getEmpresa);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        public async void Delete(string nr_cnpj)
        {
            var getEmpresa = await _context.Empresa.FirstOrDefaultAsync(x => x.NrCnpj == nr_cnpj);
            if (getEmpresa == null)
            {
                throw new Exception("Empresa não encontrada");
            }
            else
            {
                _context.Empresa.Remove(getEmpresa);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Models.Empresa>> GetAll()
        {
            var getEmpresa = await _context.Empresa.ToListAsync();
            if (getEmpresa == null)
            {
                throw new Exception("Nenhuma empresa cadastrada");
            }
            else
            {
                return getEmpresa;
            }
        }

        public async Task<Models.Empresa> GetById(int id)
        {
            var getEmpresa = await _context.Empresa.FirstOrDefaultAsync(x => x.IdEmpresa == id);
            if (getEmpresa == null)
            {
                throw new Exception("Empresa não encontrada");
            }
            else
            {
                return getEmpresa;
            }
        }

        public async Task<List<Models.Empresa>> GetByCnpj(string nr_cnpj)
        {
            var getEmpresa = await _context.Empresa.Where(x => x.NrCnpj == nr_cnpj).ToListAsync();
            if (getEmpresa == null)
            {
                throw new Exception("Empresa não encontrada");
            }
            else
            {
                return getEmpresa;
            }
        }

        public async Task<Models.Empresa> Update(int id, EmpresaDtos empresa)
        {
            var getEmpresa = await _context.Empresa.FirstOrDefaultAsync(x => x.IdEmpresa == id);
            if (getEmpresa == null)
            {
                throw new Exception("Empresa não encontrada");
            }
            else
            {
                getEmpresa.NrCnpj = empresa.NrCnpj;
                getEmpresa.NmEmpresa = empresa.NmEmpresa;
                getEmpresa.NrTelefone = empresa.NrTelefone;
                getEmpresa.DsEspecialidade = empresa.DsEspecialidade;
                await _context.SaveChangesAsync();
                return getEmpresa;
            }
        }
    }
}
