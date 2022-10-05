using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwitchSupport.DataLayer.Migrations
{
    public partial class UpdateEmailSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailSettings",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "bhrghgjiayznrjiv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmailSettings",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "vzofbkrrnigcwit");
        }
    }
}
