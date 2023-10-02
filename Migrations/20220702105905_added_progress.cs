using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class added_progress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Phase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Progress1",
                table: "Phase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Progress2",
                table: "Phase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Progress3",
                table: "Phase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Phase_Programmer",
                table: "Phase",
                column: "Programmer");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Employees_Programmer",
                table: "Phase",
                column: "Programmer",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Employees_Programmer",
                table: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Phase_Programmer",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Progress1",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Progress2",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Progress3",
                table: "Phase");
        }
    }
}
