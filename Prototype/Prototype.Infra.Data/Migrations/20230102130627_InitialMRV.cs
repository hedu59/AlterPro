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

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ContactId",
                table: "Invitations",
                column: "ContactId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
