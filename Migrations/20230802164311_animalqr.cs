using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class animalqr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_Animals_AnimalId",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_AnimalId",
                table: "Vaccines");

            migrationBuilder.AddColumn<string>(
                name: "QRImage",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRImage",
                table: "Animals");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_AnimalId",
                table: "Vaccines",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_Animals_AnimalId",
                table: "Vaccines",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "AnimalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
