using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class Paymentmode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PaymentMode",
                columns: new[] { "PaymentId", "IsActive", "PaymentType" },
                values: new object[] { 4, true, "Link" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMode",
                keyColumn: "PaymentId",
                keyValue: 4);
        }
    }
}
