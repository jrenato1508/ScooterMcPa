using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScootersMc.Data.Migrations
{
    public partial class AddTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembrosMc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imagem = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Nome = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Cpf = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Hierarquia = table.Column<int>(type: "int", nullable: false),
                    TipoSanguineo = table.Column<int>(type: "int", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembrosMc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContatoEmergencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembroMcId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Telefone = table.Column<string>(type: "Varchar(100)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoEmergencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContatoEmergencia_MembrosMc_MembroMcId",
                        column: x => x.MembroMcId,
                        principalTable: "MembrosMc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembroMcId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Numero = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Complemento = table.Column<string>(type: "Varchar(100)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cep = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Bairro = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "Varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_MembrosMc_MembroMcId",
                        column: x => x.MembroMcId,
                        principalTable: "MembrosMc",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContatoEmergencia_MembroMcId",
                table: "ContatoEmergencia",
                column: "MembroMcId");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_MembroMcId",
                table: "Enderecos",
                column: "MembroMcId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContatoEmergencia");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "MembrosMc");
        }
    }
}
