using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class addedaditional_programmer_demodate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DemoDate3",
                table: "Phase",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Programmer",
                table: "Phase",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks3",
                table: "Phase",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DemoDate3",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Programmer",
                table: "Phase");

            migrationBuilder.DropColumn(
                name: "Remarks3",
                table: "Phase");
        }
    }
}
