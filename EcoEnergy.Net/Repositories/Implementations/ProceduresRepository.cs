using EcoEnergy.Data;
using EcoEnergy.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace EcoEnergy.Repositories.Implementations
{
    public class ProceduresRepository : IProceduresRepository
    {
        private readonly DataContext _context;

        public ProceduresRepository(DataContext context)
        {
            _context = context;
        }

        public async Task InsertEmpresa(string nrCnpj, string nmEmpresa, string nrTelefone, string dsEspecialidade)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "BEGIN insert_empresa(:nrCnpj, :nmEmpresa, :nrTelefone, :dsEspecialidade); END;",
                new[]
                {
                    new OracleParameter("nrCnpj", nrCnpj),
                    new OracleParameter("nmEmpresa", nmEmpresa),
                    new OracleParameter("nrTelefone", nrTelefone),
                    new OracleParameter("dsEspecialidade", dsEspecialidade)
                });
        }

        public async Task InsertUsuario(string nmUsuario, string dsEmail, string nrTelefone, string nrCpf, int? idEmpresa = null)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "BEGIN insert_usuario(:nmUsuario, :dsEmail, :nrTelefone, :nrCpf, :idEmpresa); END;",
                new[]
                {
                    new OracleParameter("nmUsuario", nmUsuario),
                    new OracleParameter("dsEmail", dsEmail),
                    new OracleParameter("nrTelefone", nrTelefone),
                    new OracleParameter("nrCpf", nrCpf),
                    new OracleParameter("idEmpresa", idEmpresa.HasValue ? idEmpresa : DBNull.Value)
                });
        }

        public async Task InsertEndereco(string dsCep, string dsLogradouro, int nrLogradouro, string dsBairro, string dsCidade, string dsEstado, string dsPais, int? idUsuario = null, int? idEmpresa = null)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "BEGIN insert_endereco(:dsCep, :dsLogradouro, :nrLogradouro, :dsBairro, :dsCidade, :dsEstado, :dsPais, :idUsuario, :idEmpresa); END;",
                new[]
                {
                    new OracleParameter("dsCep", dsCep),
                    new OracleParameter("dsLogradouro", dsLogradouro),
                    new OracleParameter("nrLogradouro", nrLogradouro),
                    new OracleParameter("dsBairro", dsBairro),
                    new OracleParameter("dsCidade", dsCidade),
                    new OracleParameter("dsEstado", dsEstado),
                    new OracleParameter("dsPais", dsPais),
                    new OracleParameter("idUsuario", idUsuario.HasValue ? idUsuario : DBNull.Value),
                    new OracleParameter("idEmpresa", idEmpresa.HasValue ? idEmpresa : DBNull.Value)
                });
        }

        public async Task InsertSimulacao(decimal nrCustoEstimado, decimal nrEconomia, DateTime dtSimulacao, int nrConsumoMensal, int nrAreaPlaca, int nrPotenciaEstimada, int nrProducaoMensal, int nrTempoRetornoInvestimento, int dsOrcamentoSolicitado, int idUsuario, int idEndereco)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "BEGIN insert_simulacao(:nrCustoEstimado, :nrEconomia, :dtSimulacao, :nrConsumoMensal, :nrAreaPlaca, :nrPotenciaEstimada, :nrProducaoMensal, :nrTempoRetornoInvestimento, :dsOrcamentoSolicitado, :idUsuario, :idEndereco); END;",
                new[]
                {
                    new OracleParameter("nrCustoEstimado", nrCustoEstimado),
                    new OracleParameter("nrEconomia", nrEconomia),
                    new OracleParameter("dtSimulacao", dtSimulacao),
                    new OracleParameter("nrConsumoMensal", nrConsumoMensal),
                    new OracleParameter("nrAreaPlaca", nrAreaPlaca),
                    new OracleParameter("nrPotenciaEstimada", nrPotenciaEstimada),
                    new OracleParameter("nrProducaoMensal", nrProducaoMensal),
                    new OracleParameter("nrTempoRetornoInvestimento", nrTempoRetornoInvestimento),
                    new OracleParameter("dsOrcamentoSolicitado", dsOrcamentoSolicitado),
                    new OracleParameter("idUsuario", idUsuario),
                    new OracleParameter("idEndereco", idEndereco)
                });
        }

        public async Task InsertOrcamento(decimal nrValorProposto, string dsPrazo, DateTime dtOrcamento, string dsServicos, int idSimulacao, int idEmpresa)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "BEGIN insert_orcamento(:nrValorProposto, :dsPrazo, :dtOrcamento, :dsServicos, :idSimulacao, :idEmpresa); END;",
                new[]
                {
                    new OracleParameter("nrValorProposto", nrValorProposto),
                    new OracleParameter("dsPrazo", dsPrazo),
                    new OracleParameter("dtOrcamento", dtOrcamento),
                    new OracleParameter("dsServicos", dsServicos),
                    new OracleParameter("idSimulacao", idSimulacao),
                    new OracleParameter("idEmpresa", idEmpresa)
                });
        }
    }
}
