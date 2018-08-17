using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wcindicator.api.Migrations
{
    public partial class seedreportdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StatusUpdates",
                columns: new[] { "Id", "ReportTime", "Status" },
                values: new object[] { 1L, new DateTime(2018, 8, 17, 19, 55, 16, 79, DateTimeKind.Local), 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusUpdates",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
