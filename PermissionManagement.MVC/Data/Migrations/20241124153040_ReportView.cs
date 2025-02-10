using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Data.Migrations
{
    public partial class ReportView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reportViewModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalMeals = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportViewModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reportViewModels");
        }
    }
}
