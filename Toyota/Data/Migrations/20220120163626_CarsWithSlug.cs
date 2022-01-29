using Microsoft.EntityFrameworkCore.Migrations;

namespace Toyota.Data.Migrations
{
    public partial class CarsWithSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Modifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "ModificationColors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Models",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Modifications");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "ModificationColors");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Colors");
        }
    }
}
