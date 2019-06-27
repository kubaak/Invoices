using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoices.Migrations
{
    public partial class status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoicePayingStatus",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfIssue", "DueDate" },
                values: new object[] { new DateTime(2019, 6, 16, 16, 22, 21, 789, DateTimeKind.Local).AddTicks(7017), new DateTime(2019, 6, 30, 16, 22, 21, 792, DateTimeKind.Local).AddTicks(3867) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoicePayingStatus",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfIssue", "DueDate" },
                values: new object[] { new DateTime(2019, 6, 16, 11, 45, 27, 475, DateTimeKind.Local).AddTicks(8517), new DateTime(2019, 6, 30, 11, 45, 27, 477, DateTimeKind.Local).AddTicks(7115) });
        }
    }
}
