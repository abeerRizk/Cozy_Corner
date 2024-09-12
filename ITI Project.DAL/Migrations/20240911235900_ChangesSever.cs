using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangesSever : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "follows",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "follows");
        }
    }
}
