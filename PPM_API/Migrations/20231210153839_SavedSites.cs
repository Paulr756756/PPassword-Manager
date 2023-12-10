using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PPM_API.Migrations
{
    /// <inheritdoc />
    public partial class SavedSites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SavedSites",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SavedSites",
                table: "AspNetUsers");
        }
    }
}
