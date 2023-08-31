using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class revizeTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalType_AnimalTypeId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Users_UserId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Sickess_Animals_AnimalId",
                table: "Sickess");

            migrationBuilder.DropIndex(
                name: "IX_Sickess_AnimalId",
                table: "Sickess");

            migrationBuilder.DropIndex(
                name: "IX_Animals_AnimalTypeId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_UserId",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "SickessId",
                table: "Sickess",
                newName: "SicknessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SicknessId",
                table: "Sickess",
                newName: "SickessId");

            migrationBuilder.CreateIndex(
                name: "IX_Sickess_AnimalId",
                table: "Sickess",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_AnimalTypeId",
                table: "Animals",
                column: "AnimalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_UserId",
                table: "Animals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalType_AnimalTypeId",
                table: "Animals",
                column: "AnimalTypeId",
                principalTable: "AnimalType",
                principalColumn: "AnimalTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Users_UserId",
                table: "Animals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sickess_Animals_AnimalId",
                table: "Sickess",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
