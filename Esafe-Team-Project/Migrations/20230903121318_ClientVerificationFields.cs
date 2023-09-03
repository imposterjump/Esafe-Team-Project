using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esafe_Team_Project.Migrations
{
    /// <inheritdoc />
    public partial class ClientVerificationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "OTP",
                table: "Clients",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiry",
                table: "Clients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "RemainingAttempts",
                table: "Clients",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<short>(
                name: "OTP",
                table: "Admins",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OTPExpiry",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<short>(
                name: "RemainingAttempts",
                table: "Admins",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Admins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OTP",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OTPExpiry",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RemainingAttempts",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "OTPExpiry",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "RemainingAttempts",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Admins");
        }
    }
}
