using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enquiry.Web.Migrations
{
    public partial class journalstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Publication",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JournalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalStatus", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "JournalStatus",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Submitted" },
                    { 2, true, "Rejected" },
                    { 3, true, "Major" },
                    { 4, true, "Minor" },
                    { 5, true, "Accepted" },
                    { 6, true, "Published" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publication_Status",
                table: "Publication",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_JournalStatus_Status",
                table: "Publication",
                column: "Status",
                principalTable: "JournalStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publication_JournalStatus_Status",
                table: "Publication");

            migrationBuilder.DropTable(
                name: "JournalStatus");

            migrationBuilder.DropIndex(
                name: "IX_Publication_Status",
                table: "Publication");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Publication",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
