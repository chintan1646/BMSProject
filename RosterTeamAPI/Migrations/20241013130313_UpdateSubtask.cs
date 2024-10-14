using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterTeamAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "EmployeeVendorName",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "RepeatEvery",
                table: "Subtasks");

            migrationBuilder.DropColumn(
                name: "RepeatUntil",
                table: "Subtasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "Subtasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Subtasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Subtasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeVendorName",
                table: "Subtasks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RepeatEvery",
                table: "Subtasks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RepeatUntil",
                table: "Subtasks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
