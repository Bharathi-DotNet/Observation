using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class enquiryref : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnquiryRef",
                table: "Clients",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnquiryRef",
                table: "Clients");
        }
    }
}
