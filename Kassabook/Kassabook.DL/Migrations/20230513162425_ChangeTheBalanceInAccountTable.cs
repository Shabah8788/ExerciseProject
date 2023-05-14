using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kassabook.DL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTheBalanceInAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0e0e6b0e-1428-4129-8e22-357395e45474"),
                column: "Balance",
                value: 2000m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("122f4071-1d4e-4df2-9b8b-2fea2579d66e"),
                column: "Balance",
                value: 1500m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4b8ee890-1b36-470b-bd61-6edaa7588158"),
                column: "Balance",
                value: 3000m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f6e4f60-c198-42f4-a96a-ef7f2679e3ed"),
                column: "Balance",
                value: 1300m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("0e0e6b0e-1428-4129-8e22-357395e45474"),
                column: "Balance",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("122f4071-1d4e-4df2-9b8b-2fea2579d66e"),
                column: "Balance",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("4b8ee890-1b36-470b-bd61-6edaa7588158"),
                column: "Balance",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("8f6e4f60-c198-42f4-a96a-ef7f2679e3ed"),
                column: "Balance",
                value: 0m);
        }
    }
}
