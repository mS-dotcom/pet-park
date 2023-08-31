using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class complicatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Users",
                newName: "Lng");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AnimalWalkers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AnimalWalkers");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Users",
                newName: "ImageUrl");
        }
    }
}
