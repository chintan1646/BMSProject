using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterTeamAPI.Migrations
{
    /// <inheritdoc />
    public partial class TeamEmployeeaddindexing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TeamEmployees_TeamId",
                table: "TeamEmployees",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeamEmployees_TeamId",
                table: "TeamEmployees");
        }
    }
}
