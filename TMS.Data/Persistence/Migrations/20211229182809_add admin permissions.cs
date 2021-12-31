using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.Infrastructure.Persistence.Migrations
{
    public partial class addadminpermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permission", "Task_Create", "8d552bbc-0c87-4c4d-905a-2b4ea539f40a" },
                    { 2, "Permission", "Task_Update", "8d552bbc-0c87-4c4d-905a-2b4ea539f40a" },
                    { 3, "Permission", "Task_Delete", "8d552bbc-0c87-4c4d-905a-2b4ea539f40a" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d552bbc-0c87-4c4d-905a-2b4ea539f40a",
                column: "ConcurrencyStamp",
                value: "f0315bd6-7c8a-4d39-9dac-ea147f7fb0bb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "19a5ddda-09a1-48dc-8e2c-ef193ab340e4", "AQAAAAEAACcQAAAAEE+NcFXqiB2splcfA3Cjb06oKCAbyUsSXfYg7W4qEKYJrlFKLGhU0w6Eebn4uAzrXQ==", "4ac16fe8-f603-4436-9958-8dbf060e2f44" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d552bbc-0c87-4c4d-905a-2b4ea539f40a",
                column: "ConcurrencyStamp",
                value: "ae104317-728b-4c19-8fd8-3d5cdc0adb6c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bfdaf111-fdd7-4e70-b72c-76b62fdb357c", "AQAAAAEAACcQAAAAEM1d6NYDBx4UQatNYDBdC+sF9cE1tcxUQeuzux/CiJiqelBe0CQ9k/q3mCVqZuwvng==", "e1a84975-c7dd-4d2e-88f6-d0a329e733e4" });
        }
    }
}
