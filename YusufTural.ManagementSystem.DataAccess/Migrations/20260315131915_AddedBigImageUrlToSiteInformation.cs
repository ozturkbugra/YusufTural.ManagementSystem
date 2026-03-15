using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YusufTural.ManagementSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedBigImageUrlToSiteInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BigImageUrl",
                table: "SiteInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BigImageUrl",
                table: "SiteInformations");
        }
    }
}
