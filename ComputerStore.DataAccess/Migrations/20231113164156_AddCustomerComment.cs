using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerComment",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerComment",
                table: "Orders");
        }
    }
}
