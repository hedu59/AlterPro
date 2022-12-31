using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prototype.Infra.Data.Migrations
{
    public partial class InitialMRV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Servidores",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    CPF = table.Column<string>(maxLength: 11, nullable: false),
                    Orgao = table.Column<string>(maxLength: 80, nullable: false),
                    Matricula = table.Column<string>(maxLength: 40, nullable: false),
                    Setor_Atual = table.Column<int>(nullable: false),
                    SetorDescricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Login = table.Column<string>(maxLength: 50, nullable: false, defaultValue: "Admin"),
                    Email = table.Column<string>(maxLength: 100, nullable: false, defaultValue: "admin@prototype.com"),
                    Password = table.Column<string>(maxLength: 15, nullable: false, defaultValue: "123456")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ContactId = table.Column<long>(nullable: false),
                    Number = table.Column<string>(maxLength: 20, nullable: true),
                    Complement = table.Column<string>(maxLength: 100, nullable: true),
                    Neighborhood = table.Column<string>(maxLength: 250, nullable: true),
                    Street = table.Column<string>(maxLength: 250, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 80, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 16, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Category = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ServidorId = table.Column<long>(nullable: false),
                    Nome_Arquivo = table.Column<string>(maxLength: 200, nullable: false),
                    Tamanho_Arquivo = table.Column<string>(nullable: false),
                    Tipo = table.Column<string>(nullable: false, defaultValue: "application/pdf"),
                    Ultima_Modificacao = table.Column<DateTime>(nullable: false),
                    Arquivo_Base64 = table.Column<string>(nullable: false),
                    Bytes = table.Column<byte[]>(nullable: false),
                    Categoria = table.Column<int>(nullable: false),
                    CategoriaDescicao = table.Column<string>(nullable: true),
                    ServidorId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Servidores_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_Servidores_ServidorId1",
                        column: x => x.ServidorId1,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tramitacao",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ServidorId = table.Column<long>(nullable: false),
                    DocumentoId = table.Column<long>(nullable: true),
                    Data_Tramitacao = table.Column<DateTime>(nullable: false),
                    Setor_Origem = table.Column<int>(nullable: false),
                    Setor_Destino = table.Column<int>(nullable: false),
                    Setor_Origem_Descricao = table.Column<string>(nullable: true),
                    Setor_Destino_Descricao = table.Column<string>(nullable: true),
                    Usuario = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramitacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tramitacao_Servidores_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tramitacao_Servidores_ServidorId",
                        column: x => x.ServidorId,
                        principalTable: "Servidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ServidorId",
                table: "Documentos",
                column: "ServidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_ServidorId1",
                table: "Documentos",
                column: "ServidorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ContactId",
                table: "Invitations",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_DocumentoId",
                table: "Tramitacao",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tramitacao_ServidorId",
                table: "Tramitacao",
                column: "ServidorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Tramitacao");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Servidores");
        }
    }
}
