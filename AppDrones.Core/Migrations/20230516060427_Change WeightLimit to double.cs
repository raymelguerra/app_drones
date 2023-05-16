using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDrones.Core.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeightLimittodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WeightLimit",
                table: "Drone",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WeightLimit",
                table: "Drone",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
