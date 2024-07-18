using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreRL.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_UserId",
                table: "CustomerDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_UserId",
                table: "CustomerDetails",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerDetails_UserId",
                table: "CustomerDetails");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDetails_UserId",
                table: "CustomerDetails",
                column: "UserId",
                unique: true);
        }
    }
}
