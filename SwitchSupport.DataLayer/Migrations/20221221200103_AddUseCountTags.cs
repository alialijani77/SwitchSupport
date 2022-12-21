using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSupport.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUseCountTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UseCount",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseCount",
                table: "Tags");
        }
    }
}
