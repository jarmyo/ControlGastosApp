using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControlGastos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriesAndInstallments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDomiciled",
                table: "RecurringExpenses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "RecurringExpenses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalOccurrences",
                table: "RecurringExpenses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Payments",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDomiciled",
                table: "RecurringExpenses");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "RecurringExpenses");

            migrationBuilder.DropColumn(
                name: "TotalOccurrences",
                table: "RecurringExpenses");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Payments");
        }
    }
}
