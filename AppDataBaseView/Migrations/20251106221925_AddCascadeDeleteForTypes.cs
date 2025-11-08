using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDataBaseView.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаляем старые внешние ключи
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Employees_EmployeeCode",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Loads_LoadCode",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Loads_TypesLoads_LoadTypeCode",
                table: "Loads");

            migrationBuilder.DropForeignKey(
                name: "FK_TypesLoads_TypesAutos_AutoTypeCode",
                table: "TypesLoads");

            // Создаем новые внешние ключи с каскадным удалением
            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Employees_EmployeeCode",
                table: "Flights",
                column: "EmployeeCode",
                principalTable: "Employees",
                principalColumn: "EmployeeCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Loads_LoadCode",
                table: "Flights",
                column: "LoadCode",
                principalTable: "Loads",
                principalColumn: "LoadCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loads_TypesLoads_LoadTypeCode",
                table: "Loads",
                column: "LoadTypeCode",
                principalTable: "TypesLoads",
                principalColumn: "LoadTypeCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TypesLoads_TypesAutos_AutoTypeCode",
                table: "TypesLoads",
                column: "AutoTypeCode",
                principalTable: "TypesAutos",
                principalColumn: "AutoTypeCode",
                onDelete: ReferentialAction.Cascade);
        }
        

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Loads");

            migrationBuilder.DropTable(
                name: "TypesLoads");

            migrationBuilder.DropTable(
                name: "TypesAuto");
        }
    }
}
