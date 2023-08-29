using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Esafe_Team_Project.Migrations
{
    /// <inheritdoc />
    public partial class addinggbalanceandstartingtosendmoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "balance",
                table: "Clients",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "balance",
                table: "Clients");
        }
    }
}
