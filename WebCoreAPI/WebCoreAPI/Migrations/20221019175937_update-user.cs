using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCoreAPI.Migrations
{
    public partial class updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "AppUser",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AppUser",
                newName: "FullName");

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstTimeLogin",
                table: "AppUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UseType",
                table: "AppUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFirstTimeLogin",
                table: "AppUser");

            migrationBuilder.DropColumn(
                name: "UseType",
                table: "AppUser");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AppUser",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AppUser",
                newName: "FirstName");
        }
    }
}
