using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RosterTeamAPI.Migrations
{
    /// <inheritdoc />
    public partial class TeamEmployeetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TeamId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "TeamEmployees",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamEmployees", x => new { x.TeamId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_TeamEmployees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamEmployees_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamEmployees_EmployeeId",
                table: "TeamEmployees",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamEmployees");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamId",
                table: "Employees",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Teams_TeamId",
                table: "Employees",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
