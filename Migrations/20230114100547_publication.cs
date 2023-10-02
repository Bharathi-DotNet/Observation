using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class publication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedDate",
                table: "Publication",
                newName: "DateOfSubmission");

            migrationBuilder.RenameColumn(
                name: "Remarks",
                table: "Publication",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "Publication",
                newName: "Reason");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Publication",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Publication");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Publication",
                newName: "Remarks");

            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Publication",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "DateOfSubmission",
                table: "Publication",
                newName: "UploadedDate");
        }
    }
}
