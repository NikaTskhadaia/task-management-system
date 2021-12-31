using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.Infrastructure.Persistence.Migrations
{
    public partial class adddeletedfieldtoentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Tasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d552bbc-0c87-4c4d-905a-2b4ea539f40a",
                column: "ConcurrencyStamp",
                value: "7828c43b-6f22-4e20-a7cc-35b6a587ee30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a357e479-4340-4172-874a-9417b5bbb3de", "AQAAAAEAACcQAAAAEPIIeeBTuw+tjftmrJCEXADh21kxAqkGvTiuBpYVfj4Q2PyUzofX3mECthz7doXlVw==", "d3c40b6b-4075-4a6d-8564-4738999859df" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d552bbc-0c87-4c4d-905a-2b4ea539f40a",
                column: "ConcurrencyStamp",
                value: "23e90ee6-732e-4b26-b89f-7531936d92e7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34e8ab89-ae6a-4064-b832-453e2cc37528", "AQAAAAEAACcQAAAAEI8mUXw0lxzCFpddQEqxLdQHjO8J7YjahwOo20gvBDmC/Icj0t5nN7S7Ku79LWhH4A==", "04b3830f-f7cf-46cf-9075-1422b150dca7" });
        }
    }
}
