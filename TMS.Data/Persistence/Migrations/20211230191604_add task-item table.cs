using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.Infrastructure.Persistence.Migrations
{
    public partial class addtaskitemtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePaths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

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
    }
}
