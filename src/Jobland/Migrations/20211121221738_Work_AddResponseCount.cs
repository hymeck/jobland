using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobland.Migrations
{
    public partial class Work_AddResponseCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkDetails");

            migrationBuilder.AddColumn<long>(
                name: "ResponseCount",
                table: "Works",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponseCount",
                table: "Works");

            migrationBuilder.CreateTable(
                name: "WorkDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkId = table.Column<long>(type: "bigint", nullable: false),
                    Responses = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDetails_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkDetails_WorkId",
                table: "WorkDetails",
                column: "WorkId");
        }
    }
}
