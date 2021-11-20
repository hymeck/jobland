using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobland.Migrations
{
    public partial class Work_AddRelatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "Works",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "SubcategoryId",
                table: "Works",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Works_CategoryId",
                table: "Works",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_SubcategoryId",
                table: "Works",
                column: "SubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Subcategories_SubcategoryId",
                table: "Works",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Subcategories_SubcategoryId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_CategoryId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_Works_SubcategoryId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Works");
        }
    }
}
