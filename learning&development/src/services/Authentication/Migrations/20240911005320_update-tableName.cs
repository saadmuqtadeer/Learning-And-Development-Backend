using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authentication.Migrations
{
    /// <inheritdoc />
    public partial class updatetableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_registers",
                table: "registers");

            migrationBuilder.RenameTable(
                name: "registers",
                newName: "Registers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Registers",
                table: "Registers",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Registers",
                table: "Registers");

            migrationBuilder.RenameTable(
                name: "Registers",
                newName: "registers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_registers",
                table: "registers",
                column: "EmployeeId");
        }
    }
}
