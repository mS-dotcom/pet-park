using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class addComingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "PetHotels");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "PetHotels",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PetHotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PetHotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AnimalTypeText",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PetHotels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PetHotels");

            migrationBuilder.DropColumn(
                name: "AnimalTypeText",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "PetHotels",
                newName: "Lng");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "PetHotels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
