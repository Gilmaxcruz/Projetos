using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mantimentos.App.Data.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaNome = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MantimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<double>(type: "float", nullable: false),
                    TipoMovimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TpMantimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Obrigatorio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TpMantimentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadeMedidas",
                columns: table => new
                {
                    Sigla = table.Column<string>(type: "varchar(100)", nullable: false),
                    Unidade = table.Column<string>(type: "varchar(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedidas", x => x.Sigla);
                });

            migrationBuilder.CreateTable(
                name: "TpMantimentoCategorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MantimentoTpId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TpMantimentoCategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TpMantimentoCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TpMantimentoCategorias_Categorias_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TpMantimentoCategorias_TpMantimentos_MantimentoTpId",
                        column: x => x.MantimentoTpId,
                        principalTable: "TpMantimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mantimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMantimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MarcaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeSigla = table.Column<string>(type: "varchar(100)", nullable: true),
                    Validade = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacidade = table.Column<string>(type: "varchar(200)", nullable: false),
                    Imagem = table.Column<string>(type: "varchar(100)", nullable: false),
                    ConteudoAtual = table.Column<string>(type: "varchar(200)", nullable: false),
                    Estoque = table.Column<double>(type: "float", nullable: false),
                    EstoqueMin = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantimentos_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantimentos_TpMantimentos_TipoMantimentoId",
                        column: x => x.TipoMantimentoId,
                        principalTable: "TpMantimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mantimentos_UnidadeMedidas_UnidadeSigla",
                        column: x => x.UnidadeSigla,
                        principalTable: "UnidadeMedidas",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MantimentoMovimento",
                columns: table => new
                {
                    MantimentosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovimentosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantimentoMovimento", x => new { x.MantimentosId, x.MovimentosId });
                    table.ForeignKey(
                        name: "FK_MantimentoMovimento_Mantimentos_MantimentosId",
                        column: x => x.MantimentosId,
                        principalTable: "Mantimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MantimentoMovimento_Movimentos_MovimentosId",
                        column: x => x.MovimentosId,
                        principalTable: "Movimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MantimentoMovimento_MovimentosId",
                table: "MantimentoMovimento",
                column: "MovimentosId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantimentos_MarcaId",
                table: "Mantimentos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantimentos_TipoMantimentoId",
                table: "Mantimentos",
                column: "TipoMantimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantimentos_UnidadeSigla",
                table: "Mantimentos",
                column: "UnidadeSigla");

            migrationBuilder.CreateIndex(
                name: "IX_TpMantimentoCategorias_CategoriaId",
                table: "TpMantimentoCategorias",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_TpMantimentoCategorias_CategoriaId1",
                table: "TpMantimentoCategorias",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_TpMantimentoCategorias_MantimentoTpId",
                table: "TpMantimentoCategorias",
                column: "MantimentoTpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MantimentoMovimento");

            migrationBuilder.DropTable(
                name: "TpMantimentoCategorias");

            migrationBuilder.DropTable(
                name: "Mantimentos");

            migrationBuilder.DropTable(
                name: "Movimentos");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "TpMantimentos");

            migrationBuilder.DropTable(
                name: "UnidadeMedidas");
        }
    }
}
