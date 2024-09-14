using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
