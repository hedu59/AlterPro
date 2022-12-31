using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Infra.Data.Seeds
{
    /// <summary>
    /// This ordenation can not change
    /// </summary>
    public static class InitialSeed
    {
        public static void CargaSeed(this MigrationBuilder migrationBuilder)
        {
            InsertContacts(migrationBuilder);
            InsertInvitations(migrationBuilder);
        }
        private static void InsertContacts(MigrationBuilder migrationBuilder)
        {
            var sql = SeedDataBase.InsertContatcs;

            migrationBuilder.Sql(sql);
        }

        private static void InsertInvitations(MigrationBuilder migrationBuilder)
        {
            var sql = SeedDataBase.InsertInvitations;

            migrationBuilder.Sql(sql);
        }
    }
}
