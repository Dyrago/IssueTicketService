using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssueTicketService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Users");
        }
    }
}
