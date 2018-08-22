using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wcindicator.api.Migrations
{
    public partial class addsstatusduration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "StatusDuration",
                table: "StatusUpdates",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusDuration",
                table: "StatusUpdates");
        }
    }
}
