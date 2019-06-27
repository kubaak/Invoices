using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Invoices.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Subscribers_TotalId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_TotalId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Subscribers",
                newName: "Address");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Invoices",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "Id", "Address", "Dic", "Email", "Ic", "Name", "Phone" },
                values: new object[] { 1, "Some Address", null, null, null, "Test subscriber", null });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "BankAccount", "Dic", "Email", "Ic", "Name", "Phone" },
                values: new object[] { 1, "Some Address", null, null, null, null, "Test supplier", null });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "DateOfIssue", "DueDate", "SubscriberId", "SupplierId", "Total" },
                values: new object[] { 1, new DateTime(2019, 6, 16, 11, 45, 27, 475, DateTimeKind.Local).AddTicks(8517), new DateTime(2019, 6, 30, 11, 45, 27, 477, DateTimeKind.Local).AddTicks(7115), 1, 1, 99m });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Amount", "Description", "InvoiceId", "UnitPrice" },
                values: new object[] { 1, 1m, "Some goods", 1, 99m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscribers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Subscribers",
                newName: "Adress");

            migrationBuilder.AddColumn<int>(
                name: "TotalId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TotalId",
                table: "Invoices",
                column: "TotalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Subscribers_TotalId",
                table: "Invoices",
                column: "TotalId",
                principalTable: "Subscribers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
