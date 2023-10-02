using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class addedworkstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 6,
                column: "WorkStatusName",
                value: "Accepted");

            migrationBuilder.UpdateData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 7,
                column: "WorkStatusName",
                value: "Rejected");

            migrationBuilder.InsertData(
                table: "WorkStatus",
                columns: new[] { "WorkStatusId", "IsActive", "WorkStatusName" },
                values: new object[] { 8, true, "Completed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 6,
                column: "WorkStatusName",
                value: "Published");

            migrationBuilder.UpdateData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 7,
                column: "WorkStatusName",
                value: "Completed");
        }
    }
}
