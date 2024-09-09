using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationService.Migrations
{
    /// <inheritdoc />
    public partial class trainingRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Register",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "trainingRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestorId = table.Column<int>(type: "int", nullable: false),
                    RequestorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestorEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrainingDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    TrainingTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TrainingDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NumberOfEmployees = table.Column<int>(type: "int", nullable: false),
                    TechnicalSkillSetRequired = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    PreferredStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SpecialRequirements = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainingRequests_Register_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "Register",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trainingRequests_RequestorId",
                table: "trainingRequests",
                column: "RequestorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trainingRequests");

            migrationBuilder.DropTable(
                name: "Register");
        }
    }
}
