using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class addedaditional_techexpert_demodate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DemoDate1",
                table: "Phase",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DemoDate2",
                table: "Phase",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Phase",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks1",
                table: "Phase",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remarks2",
                table: "Phase",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TechExpert1",
                table: "Phase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechExpert2",
                table: "Phase",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Phase_TechExpert1",
                table: "Phase",
                column: "TechExpert1");

            migrationBuilder.CreateIndex(
                name: "IX_Phase_TechExpert2",
                table: "Phase",
                column: "TechExpert2");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Employees_TechExpert1",
                table: "Phase",
                column: "TechExpert1",
                principalTable: "Employees",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phase_Employees_TechExpert2",
                table: "Phase",
                column: "TechExpert2",
                principalTable: "Employees",
                principalColumn: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Employees_TechExpert1",
                table: "Phase");

            migrationBuilder.DropForeignKey(
                name: "FK_Phase_Employees_TechExpert2",
                table: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Phase_TechExpert1",
                table: "Phase");

            migrationBuilder.DropIndex(
                name: "IX_Phase_TechExpert2",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "DemoDate1",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "DemoDate2",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Remarks1",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Remarks2",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "TechExpert1",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "TechExpert2",
                table: "Phase");
        }
    }
}
