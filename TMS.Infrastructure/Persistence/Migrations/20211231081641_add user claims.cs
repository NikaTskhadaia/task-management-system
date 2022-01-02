using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.Infrastructure.Persistence.Migrations
{
    public partial class adduserclaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "2634a5f0-c9d3-43b4-8d7d-04b529201146");

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "Permission", "Task_Create", "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee" },
                    { 2, "Permission", "Task_Update", "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee" },
                    { 3, "Permission", "Task_Delete", "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4dee5369-26f6-4347-9903-4bd671a7c7e1", "AQAAAAEAACcQAAAAEMyxk/zfIECkgPsqJxudiUhQ8qdVPHeFlQ/U2EePZuPpyKTZj+pwpEqCiA3X/Em0MQ==", "e907285c-3837-43a6-adb1-df2bc2d3c76a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

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
                value: "7828c43b-6f22-4e20-a7cc-35b6a587ee30");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a357e479-4340-4172-874a-9417b5bbb3de", "AQAAAAEAACcQAAAAEPIIeeBTuw+tjftmrJCEXADh21kxAqkGvTiuBpYVfj4Q2PyUzofX3mECthz7doXlVw==", "d3c40b6b-4075-4a6d-8564-4738999859df" });
        }
    }
}
