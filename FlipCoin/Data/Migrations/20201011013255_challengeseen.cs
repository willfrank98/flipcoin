using Microsoft.EntityFrameworkCore.Migrations;

namespace FlipCoin.Data.Migrations
{
    public partial class challengeseen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Challenges",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Challenges");
        }
    }
}
