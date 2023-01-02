using Microsoft.EntityFrameworkCore.Migrations;
using Prototype.Infra.Data.Seeds;

namespace Prototype.Infra.Data.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.LoadSeed();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
