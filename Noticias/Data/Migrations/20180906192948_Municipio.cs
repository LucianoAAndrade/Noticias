using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Noticias.Data.Migrations
{
    public partial class Municipio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipio",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 100, nullable: true),
                    Uf = table.Column<string>(maxLength: 2, nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipio", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Noticias",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Autor = table.Column<string>(nullable: true),
                    CategoriaId = table.Column<int>(nullable: true),
                    Corpo = table.Column<string>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: true),
                    DataPublicaca = table.Column<DateTime>(nullable: true),
                    Destaque = table.Column<bool>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 256, nullable: true),
                    UsuarioAlteracao = table.Column<int>(nullable: false),
                    UsuarioCriacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Noticias", x => x.id);
                    table.ForeignKey(
                        name: "FK_Noticias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_CategoriaId",
                table: "Noticias",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Municipio");

            migrationBuilder.DropTable(
                name: "Noticias");
        }
    }
}
