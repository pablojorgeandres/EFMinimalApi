using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFMinimalApi.Migrations
{
    public partial class Column_ImportanceLevel_AddedToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ImportanceLevel",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportanceLevel",
                table: "Category");
        }
    }
}
