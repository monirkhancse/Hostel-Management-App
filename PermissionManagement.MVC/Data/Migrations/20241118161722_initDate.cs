using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Data.Migrations
{
    public partial class initDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Meals",
                newName: "EntryDate");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Expenses",
                newName: "ExpenseDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Deposits",
                newName: "DepositDate");

            migrationBuilder.AlterColumn<decimal>(
                name: "TodayMeal",
                table: "Meals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "Meals",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "ExpenseDate",
                table: "Expenses",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "DepositDate",
                table: "Deposits",
                newName: "CreatedDate");

            migrationBuilder.AlterColumn<int>(
                name: "TodayMeal",
                table: "Meals",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
