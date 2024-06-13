using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPagination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9fcddc14-5ab3-424c-bb81-a1929a64afa6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f4ac3254-a754-444f-9b85-d0285dd928c8"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2365686e-7a31-427b-b276-c4176934a51c"), null, "Admin", "ADMIN" },
                    { new Guid("699af13e-39f0-473c-8834-cab38053ab40"), null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2365686e-7a31-427b-b276-c4176934a51c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("699af13e-39f0-473c-8834-cab38053ab40"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("9fcddc14-5ab3-424c-bb81-a1929a64afa6"), null, "Customer", "CUSTOMER" },
                    { new Guid("f4ac3254-a754-444f-9b85-d0285dd928c8"), null, "Admin", "ADMIN" }
                });
        }
    }
}
