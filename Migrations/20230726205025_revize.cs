using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class revize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Districts_DistrictId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DistrictId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vaccines",
                newName: "VaccineId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Sickess",
                newName: "SickessId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnimalType",
                newName: "AnimalTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VaccineId",
                table: "Vaccines",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "SickessId",
                table: "Sickess",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnimalTypeId",
                table: "AnimalType",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DistrictId",
                table: "Users",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Districts_DistrictId",
                table: "Users",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                table: "Users",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
