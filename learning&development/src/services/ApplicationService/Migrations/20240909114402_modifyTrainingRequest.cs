using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationService.Migrations
{
    /// <inheritdoc />
    public partial class modifyTrainingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainingRequests_Register_RequestorId",
                table: "trainingRequests");

            migrationBuilder.RenameColumn(
                name: "RequestorId",
                table: "trainingRequests",
                newName: "EmmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_trainingRequests_RequestorId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainingRequests_Register_EmmployeeId",
                table: "trainingRequests");

            migrationBuilder.RenameColumn(
                name: "EmmployeeId",
                table: "trainingRequests",
                newName: "RequestorId");

            migrationBuilder.RenameIndex(
                name: "IX_trainingRequests_EmmployeeId",
                table: "trainingRequests",
                newName: "IX_trainingRequests_RequestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_trainingRequests_Register_RequestorId",
                table: "trainingRequests",
                column: "RequestorId",
                principalTable: "Register",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
