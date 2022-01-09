using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresentForFriend.Migrations
{
    public partial class imageInDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Presents");

            migrationBuilder.AddColumn<byte[]>(
                name: "DataFiles",
                table: "Presents",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFiles",
                table: "Presents");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Presents",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
