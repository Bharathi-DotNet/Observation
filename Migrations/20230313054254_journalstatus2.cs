using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class journalstatus2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JournalStatus",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 7, true, "MailAccount" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JournalStatus",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
