using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4c803cd7-875c-4031-b476-b64271e7164e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("98788342-e0f1-4cb1-8a1d-8dd85fd9afd1"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("00d8f440-ffd9-4107-a8d0-1b5c39731100"), null, "Admin", "ADMIN" },
                    { new Guid("829f0e92-7bff-4d19-91e6-0084a5a0b859"), null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("00d8f440-ffd9-4107-a8d0-1b5c39731100"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("829f0e92-7bff-4d19-91e6-0084a5a0b859"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4c803cd7-875c-4031-b476-b64271e7164e"), null, "Admin", "ADMIN" },
                    { new Guid("98788342-e0f1-4cb1-8a1d-8dd85fd9afd1"), null, "Customer", "CUSTOMER" }
                });
        }
    }
}
