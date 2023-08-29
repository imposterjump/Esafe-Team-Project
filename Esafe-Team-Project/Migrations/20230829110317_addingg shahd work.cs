using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esafe_Team_Project.Migrations
{
    /// <inheritdoc />
    public partial class addinggshahdwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CertificateType = table.Column<int>(type: "int", nullable: false),
                    InterestPercentage = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CardType = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BalanceAvailable = table.Column<double>(type: "float", nullable: false),
                    CardLimit = table.Column<double>(type: "float", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AcceptanceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}
