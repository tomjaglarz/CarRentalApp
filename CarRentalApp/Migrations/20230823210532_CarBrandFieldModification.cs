using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalApp.Migrations
{
    /// <inheritdoc />
    public partial class CarBrandFieldModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
