using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class backtoenquiry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BackToEnquiry",
                table: "Clients",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PreviousEnquiryId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackToEnquiry",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PreviousEnquiryId",
                table: "Clients");
        }
    }
}
