using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDrones.Core.Migrations
{
    /// <inheritdoc />
    public partial class Adduniquecontrainserialnumbertodrone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Drone_SerialNumber",
                table: "Drone",
                column: "SerialNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drone_SerialNumber",
                table: "Drone");
        }
    }
}
