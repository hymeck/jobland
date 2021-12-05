using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobland.Infrastructure.Common.Persistence.Migrations
{
    public partial class AddTable_ProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "modified",
                table: "work",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 915, DateTimeKind.Utc).AddTicks(439));

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "work",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 915, DateTimeKind.Utc).AddTicks(85));

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified",
                table: "subcategory",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(6900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "subcategory",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(6491));

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified",
                table: "category",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(3907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "category",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(3589));

            migrationBuilder.CreateTable(
                name: "profileimages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    added = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_profileimages_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "user profile images")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_profileimages_OwnerId",
                table: "profileimages",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profileimages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified",
                table: "work",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 915, DateTimeKind.Utc).AddTicks(439),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "work",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 915, DateTimeKind.Utc).AddTicks(85),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified",
                table: "subcategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(6900),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "subcategory",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(6491),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified",
                table: "category",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(3907),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "category",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2021, 12, 4, 7, 38, 12, 914, DateTimeKind.Utc).AddTicks(3589),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
