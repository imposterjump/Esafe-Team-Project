using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esafe_Team_Project.Migrations
{
    /// <inheritdoc />
    public partial class addinggthearraysofcredsandtheresyinclient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ClientId",
                table: "Transfers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_ClientId",
                table: "CreditCards",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ClientId",
                table: "Certificates",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificates_Clients_ClientId",
                table: "Certificates",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CreditCards_Clients_ClientId",
                table: "CreditCards",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Clients_ClientId",
                table: "Transfers",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificates_Clients_ClientId",
                table: "Certificates");

            migrationBuilder.DropForeignKey(
                name: "FK_CreditCards_Clients_ClientId",
                table: "CreditCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Clients_ClientId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ClientId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_ClientId",
                table: "CreditCards");

            migrationBuilder.DropIndex(
                name: "IX_Certificates_ClientId",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Transfers");
        }
    }
}
