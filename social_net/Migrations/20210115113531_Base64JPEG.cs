using Microsoft.EntityFrameworkCore.Migrations;

namespace social_net.Migrations
{
    public partial class Base64JPEG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_URL",
                table: "images",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_URL",
                table: "images");
        }
    }
}
