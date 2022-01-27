using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApp.Migrations
{
    public partial class RemovedOfficeAndEmployeeReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Office_OfficeId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_OfficeId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "Employee");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_OfficeId",
                table: "Employee",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Office_OfficeId",
                table: "Employee",
                column: "OfficeId",
                principalTable: "Office",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
