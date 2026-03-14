using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YusufTural.ManagementSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SiteInformationAddedVideoUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "SiteInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "SiteInformations");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[] { 1, "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92", "admin" });
        }
    }
}
