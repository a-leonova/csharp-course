using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lab5.Migrations
{
    public partial class AddProjectDeadlineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(2019, 4, 30, 21, 0, 0, 0, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Projects");
        }
    }
}
