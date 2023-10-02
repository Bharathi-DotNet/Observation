using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class rel_phase_employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Employees_EmployeesEmpId",
                table: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Phase_EmployeesEmpId",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "EmployeesEmpId",
                table: "Phase");

            migrationBuilder.CreateIndex(
                name: "IX_Phase_TechExpert",
                table: "Phase",
                column: "TechExpert");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Employees_TechExpert",
                table: "Phase",
                column: "TechExpert",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Employees_TechExpert",
                table: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Phase_TechExpert",
                table: "Phase");

            migrationBuilder.AddColumn<int>(
                name: "EmployeesEmpId",
                table: "Phase",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phase_EmployeesEmpId",
                table: "Phase",
                column: "EmployeesEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Employees_EmployeesEmpId",
                table: "Phase",
                column: "EmployeesEmpId",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }
    }
}
