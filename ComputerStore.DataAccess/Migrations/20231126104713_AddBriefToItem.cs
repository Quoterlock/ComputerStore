using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBriefToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brief",
                table: "Items",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brief",
                table: "Items");
        }
    }
}
