using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITI_Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addFieldCustomerName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Invoices");
        }
    }
}
