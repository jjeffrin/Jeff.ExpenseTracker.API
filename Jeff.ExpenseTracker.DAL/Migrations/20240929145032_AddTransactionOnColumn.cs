using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jeff.ExpenseTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionOnColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionOn",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionOn",
                table: "Transactions");
        }
    }
}
