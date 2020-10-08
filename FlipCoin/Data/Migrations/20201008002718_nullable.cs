using Microsoft.EntityFrameworkCore.Migrations;

namespace FlipCoin.Data.Migrations
{
    public partial class nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Result",
                table: "Challenges",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Result",
                table: "Challenges",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
