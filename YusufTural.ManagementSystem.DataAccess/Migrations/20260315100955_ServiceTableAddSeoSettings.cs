using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YusufTural.ManagementSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ServiceTableAddSeoSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeoDescription",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoKeywords",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SeoTitle",
                table: "Services",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeoDescription",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SeoKeywords",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SeoTitle",
                table: "Services");
        }
    }
}
