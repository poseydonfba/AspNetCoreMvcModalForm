using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetCoreMvcModalForm.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoSolicitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoSolicitacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Solicitacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(100)", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric", nullable: false),
                    TipoSolicitacaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solicitacao_TipoSolicitacao_TipoSolicitacaoId",
                        column: x => x.TipoSolicitacaoId,
                        principalTable: "TipoSolicitacao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacao_TipoSolicitacaoId",
                table: "Solicitacao",
                column: "TipoSolicitacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitacao");

            migrationBuilder.DropTable(
                name: "TipoSolicitacao");
        }
    }
}
