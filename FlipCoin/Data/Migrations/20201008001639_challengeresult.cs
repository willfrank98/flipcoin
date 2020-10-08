using Microsoft.EntityFrameworkCore.Migrations;

namespace FlipCoin.Data.Migrations
{
    public partial class challengeresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Queues",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<double>(
                name: "Result",
                table: "Challenges",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Challenges");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Queues",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
