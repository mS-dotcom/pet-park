using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class renameLocationColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DistrictName",
                table: "UserDistricts",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserDistricts",
                newName: "DistrictId");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "UserCities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserCities",
                newName: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserDistricts",
                newName: "DistrictName");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "UserDistricts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserCities",
                newName: "CityName");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "UserCities",
                newName: "Id");
        }
    }
}
