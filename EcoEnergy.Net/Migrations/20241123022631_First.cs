using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcoEnergy.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_T_GS24_EMPRESA");

            migrationBuilder.CreateSequence(
                name: "SEQ_T_GS24_ENDERECO");

            migrationBuilder.CreateSequence(
                name: "SEQ_T_GS24_ORCAMENTO");

            migrationBuilder.CreateSequence(
                name: "SEQ_T_GS24_SIMULACAO");

            migrationBuilder.CreateSequence(
                name: "SEQ_T_GS24_USUARIO");

            migrationBuilder.CreateTable(
                name: "T_GS24_EMPRESA",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_T_GS24_EMPRESA.NEXTVAL"),
                    NR_CNPJ = table.Column<string>(type: "NVARCHAR2(14)", maxLength: 14, nullable: false),
                    NM_EMPRESA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    DS_ESPECIALIDADE = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS24_EMPRESA", x => x.ID_EMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "T_GS24_USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_T_GS24_USUARIO.NEXTVAL"),
                    NM_USUARIO = table.Column<string>(type: "NVARCHAR2(70)", maxLength: 70, nullable: false),
                    DS_EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NR_TELEFONE = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    NR_CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    EmpresaIdEmpresa = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS24_USUARIO", x => x.ID_USUARIO);
                    table.ForeignKey(
                        name: "FK_T_GS24_USUARIO_T_GS24_EMPRESA_EmpresaIdEmpresa",
                        column: x => x.EmpresaIdEmpresa,
                        principalTable: "T_GS24_EMPRESA",
                        principalColumn: "ID_EMPRESA");
                });

            migrationBuilder.CreateTable(
                name: "T_GS24_ENDERECO",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_T_GS24_ENDERECO.NEXTVAL"),
                    DS_CEP = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    DS_LOGRADOURO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NR_LOGRADOURO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DS_BAIRRO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_CIDADE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_ESTADO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DS_PAIS = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    UsuarioIdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    EmpresaIdEmpresa = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS24_ENDERECO", x => x.ID_ENDERECO);
                    table.ForeignKey(
                        name: "FK_T_GS24_ENDERECO_T_GS24_EMPRESA_EmpresaIdEmpresa",
                        column: x => x.EmpresaIdEmpresa,
                        principalTable: "T_GS24_EMPRESA",
                        principalColumn: "ID_EMPRESA");
                    table.ForeignKey(
                        name: "FK_T_GS24_ENDERECO_T_GS24_USUARIO_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "T_GS24_USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "T_GS24_SIMULACAO",
                columns: table => new
                {
                    ID_SIMULACAO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_T_GS24_SIMULACAO.NEXTVAL"),
                    NR_CUSTO_ESTIMADO = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    NR_ECONOMIA = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DT_SIMULACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    NR_CONSUMO_MENSAL = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    NR_AREA_PLACA = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    NR_POTENCIA_ESTIMADA = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    NR_PRODUCAO_MENSAL = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    NR_TEMPO_RETORNO_INVESTIMENTO = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DS_ORCAMENTO_SOLICITADO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_USUARIO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UsuarioIdUsuario = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EnderecoIdEndereco = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS24_SIMULACAO", x => x.ID_SIMULACAO);
                    table.ForeignKey(
                        name: "FK_T_GS24_SIMULACAO_T_GS24_ENDERECO_EnderecoIdEndereco",
                        column: x => x.EnderecoIdEndereco,
                        principalTable: "T_GS24_ENDERECO",
                        principalColumn: "ID_ENDERECO");
                    table.ForeignKey(
                        name: "FK_T_GS24_SIMULACAO_T_GS24_USUARIO_UsuarioIdUsuario",
                        column: x => x.UsuarioIdUsuario,
                        principalTable: "T_GS24_USUARIO",
                        principalColumn: "ID_USUARIO");
                });

            migrationBuilder.CreateTable(
                name: "T_GS24_ORCAMENTO",
                columns: table => new
                {
                    ID_ORCAMENTO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "SEQ_T_GS24_ORCAMENTO.NEXTVAL"),
                    NR_VALOR_PROPOSTO = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    DS_PRAZO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    DT_ORCAMENTO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DS_SERVICOS = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false),
                    ID_SIMULACAO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SimulacaoIdSimulacao = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    ID_EMPRESA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    EmpresaIdEmpresa = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_GS24_ORCAMENTO", x => x.ID_ORCAMENTO);
                    table.ForeignKey(
                        name: "FK_T_GS24_ORCAMENTO_T_GS24_EMPRESA_EmpresaIdEmpresa",
                        column: x => x.EmpresaIdEmpresa,
                        principalTable: "T_GS24_EMPRESA",
                        principalColumn: "ID_EMPRESA");
                    table.ForeignKey(
                        name: "FK_T_GS24_ORCAMENTO_T_GS24_SIMULACAO_SimulacaoIdSimulacao",
                        column: x => x.SimulacaoIdSimulacao,
                        principalTable: "T_GS24_SIMULACAO",
                        principalColumn: "ID_SIMULACAO");
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_ENDERECO_EmpresaIdEmpresa",
                table: "T_GS24_ENDERECO",
                column: "EmpresaIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_ENDERECO_UsuarioIdUsuario",
                table: "T_GS24_ENDERECO",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_ORCAMENTO_EmpresaIdEmpresa",
                table: "T_GS24_ORCAMENTO",
                column: "EmpresaIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_ORCAMENTO_SimulacaoIdSimulacao",
                table: "T_GS24_ORCAMENTO",
                column: "SimulacaoIdSimulacao");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_SIMULACAO_EnderecoIdEndereco",
                table: "T_GS24_SIMULACAO",
                column: "EnderecoIdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_SIMULACAO_UsuarioIdUsuario",
                table: "T_GS24_SIMULACAO",
                column: "UsuarioIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_T_GS24_USUARIO_EmpresaIdEmpresa",
                table: "T_GS24_USUARIO",
                column: "EmpresaIdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_GS24_ORCAMENTO");

            migrationBuilder.DropTable(
                name: "T_GS24_SIMULACAO");

            migrationBuilder.DropTable(
                name: "T_GS24_ENDERECO");

            migrationBuilder.DropTable(
                name: "T_GS24_USUARIO");

            migrationBuilder.DropTable(
                name: "T_GS24_EMPRESA");

            migrationBuilder.DropSequence(
                name: "SEQ_T_GS24_EMPRESA");

            migrationBuilder.DropSequence(
                name: "SEQ_T_GS24_ENDERECO");

            migrationBuilder.DropSequence(
                name: "SEQ_T_GS24_ORCAMENTO");

            migrationBuilder.DropSequence(
                name: "SEQ_T_GS24_SIMULACAO");

            migrationBuilder.DropSequence(
                name: "SEQ_T_GS24_USUARIO");
        }
    }
}
