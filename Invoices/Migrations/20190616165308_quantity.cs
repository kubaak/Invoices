using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoices.Migrations
{
    public partial class quantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Items",
                newName: "Quantity");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfIssue", "DueDate" },
                values: new object[] { new DateTime(2019, 6, 16, 18, 53, 7, 512, DateTimeKind.Local).AddTicks(5308), new DateTime(2019, 6, 30, 18, 53, 7, 514, DateTimeKind.Local).AddTicks(2205) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Items",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfIssue", "DueDate" },
                values: new object[] { new DateTime(2019, 6, 16, 18, 13, 22, 732, DateTimeKind.Local).AddTicks(5680), new DateTime(2019, 6, 30, 18, 13, 22, 734, DateTimeKind.Local).AddTicks(7025) });
        }
    }
}
