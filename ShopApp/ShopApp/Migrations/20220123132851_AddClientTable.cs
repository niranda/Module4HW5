using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApp.Migrations
{
    public partial class AddClientTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "Age", "FirstName", "Gender", "LastName", "OrderDate" },
                values: new object[,]
                {
                    { 1, 29, "Alex", "Male", "Brown", new DateTime(2008, 5, 1, 8, 30, 52, 0, DateTimeKind.Unspecified) },
                    { 2, 23, "Lily", "Female", "King", new DateTime(2008, 5, 1, 8, 30, 52, 0, DateTimeKind.Unspecified) },
                    { 3, 18, "Isabella", "Female", "Lewis", new DateTime(2008, 5, 1, 8, 30, 52, 0, DateTimeKind.Unspecified) },
                    { 4, 58, "George", "Male", "Wilson", new DateTime(2008, 5, 1, 8, 30, 52, 0, DateTimeKind.Unspecified) },
                    { 5, 67, "Connor", "Male", "Ellington", new DateTime(2008, 5, 1, 8, 30, 52, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ClientId",
                table: "Project",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Client_ClientId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Project_ClientId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Project");
        }
    }
}
