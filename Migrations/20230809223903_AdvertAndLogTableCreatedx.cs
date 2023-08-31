using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class AdvertAndLogTableCreatedx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adverts",
                columns: table => new
                {
                    AdvertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AdvertHeader = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdvertDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    AdvertImageLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalInfoShare = table.Column<bool>(type: "bit", nullable: true),
                    AnimalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adverts", x => x.AdvertId);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adverts");

            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
