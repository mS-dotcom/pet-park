using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class myUpdatesTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Veterinary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Gender",
                table: "Animals",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Veterinary");

            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Animals");
        }
    }
}
