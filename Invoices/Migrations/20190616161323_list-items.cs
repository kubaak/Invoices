using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoices.Migrations
{
    public partial class listitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfIssue", "DueDate" },
                values: new object[] { new DateTime(2019, 6, 16, 18, 13, 22, 732, DateTimeKind.Local).AddTicks(5680), new DateTime(2019, 6, 30, 18, 13, 22, 734, DateTimeKind.Local).AddTicks(7025) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateOfIssue", "DueDate" },
                values: new object[] { new DateTime(2019, 6, 16, 16, 22, 21, 789, DateTimeKind.Local).AddTicks(7017), new DateTime(2019, 6, 30, 16, 22, 21, 792, DateTimeKind.Local).AddTicks(3867) });
        }
    }
}
