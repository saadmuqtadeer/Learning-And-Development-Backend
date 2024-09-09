using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationService.Migrations
{
    /// <inheritdoc />
    public partial class updateTrainingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainingRequests_Register_EmmployeeId",
                table: "trainingRequests");

            migrationBuilder.RenameColumn(
                name: "EmmployeeId",
                table: "trainingRequests",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_trainingRequests_EmmployeeId",
                table: "trainingRequests",
                newName: "IX_trainingRequests_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_trainingRequests_Register_EmployeeId",
                table: "trainingRequests",
                column: "EmployeeId",
                principalTable: "Register",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainingRequests_Register_EmployeeId",
                table: "trainingRequests");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "trainingRequests",
                newName: "EmmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_trainingRequests_EmployeeId",
                table: "trainingRequests",
                newName: "IX_trainingRequests_EmmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_trainingRequests_Register_EmmployeeId",
                table: "trainingRequests",
                column: "EmmployeeId",
                principalTable: "Register",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
