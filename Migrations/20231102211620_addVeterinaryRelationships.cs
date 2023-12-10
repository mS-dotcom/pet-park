using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace perpark_api.Migrations
{
    /// <inheritdoc />
    public partial class addVeterinaryRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileLocation",
                table: "Veterinary",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasClinicPinned",
                table: "Veterinary",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFileSend",
                table: "Veterinary",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasPayment",
                table: "Veterinary",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "Veterinary",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDate",
                table: "Veterinary",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileLocation",
                table: "Veterinary");

            migrationBuilder.DropColumn(
                name: "HasClinicPinned",
                table: "Veterinary");

            migrationBuilder.DropColumn(
                name: "HasFileSend",
                table: "Veterinary");

            migrationBuilder.DropColumn(
                name: "HasPayment",
                table: "Veterinary");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "Veterinary");

            migrationBuilder.DropColumn(
                name: "LastDate",
                table: "Veterinary");
        }
    }
}
