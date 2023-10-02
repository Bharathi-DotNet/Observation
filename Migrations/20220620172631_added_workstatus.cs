using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class added_workstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 4,
                column: "WorkStatusName",
                value: "Publication");

            migrationBuilder.InsertData(
                table: "WorkStatus",
                columns: new[] { "WorkStatusId", "IsActive", "WorkStatusName" },
                values: new object[,]
                {
                    { 5, true, "Revision" },
                    { 6, true, "Published" },
                    { 7, true, "Completed" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "WorkStatus",
                keyColumn: "WorkStatusId",
                keyValue: 4,
                column: "WorkStatusName",
                value: "Completed");
        }
    }
}
