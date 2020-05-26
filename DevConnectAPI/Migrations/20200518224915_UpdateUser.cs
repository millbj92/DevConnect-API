using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevConnectAPI.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userGuid",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "verificationToken",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "verificationToken",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "userGuid",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
