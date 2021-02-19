using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace social_net.Migrations
{
    public partial class Images64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageData",
                table: "images",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "images",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
