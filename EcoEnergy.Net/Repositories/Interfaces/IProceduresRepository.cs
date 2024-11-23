namespace EcoEnergy.Repositories.Interfaces
{
    public interface IProceduresRepository
    {
        Task InsertEmpresa(string cnpj, string nomeEmpresa, string telefone, string especialidade);
        Task InsertUsuario(string nome, string email, string telefone, string cpf, int? idEmpresa = null);
        Task InsertEndereco(string cep, string logradouro, int numero, string bairro, string cidade, string estado, string pais, int? idUsuario = null, int? idEmpresa = null);
        Task InsertSimulacao(decimal custoEstimado, decimal economia, DateTime dataSimulacao, int consumoMensal, int areaPlaca, int potenciaEstimada, int producaoMensal, int tempoRetorno, int orcamentoSolicitado, int idUsuario, int idEndereco);
        Task InsertOrcamento(decimal valorProposto, string prazo, DateTime dataOrcamento, string servicos, int idSimulacao, int idEmpresa);
    }
}
