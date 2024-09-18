using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addIsCustomerField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomer",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomer",
                table: "AspNetUsers");
        }
    }
}
