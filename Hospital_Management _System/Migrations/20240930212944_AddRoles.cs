using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management__System.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "29f6d7da-6c1b-4d5a-b9d8-0c0f2d798c71", "7b1c57fc-0ef8-43aa-935c-a9c5b4662858", "User", "user" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ab7a8c72-6b36-4469-a2cb-bedb81e7e52a", "e5de0aa7-bd10-429b-a587-5e6932fc4e34", "Manager", "manager" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8711000-fc18-4fd6-860e-4ae24ddb3da6", "93498785-0701-487c-bc52-1309c1a76fb4", "Admin", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29f6d7da-6c1b-4d5a-b9d8-0c0f2d798c71");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab7a8c72-6b36-4469-a2cb-bedb81e7e52a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8711000-fc18-4fd6-860e-4ae24ddb3da6");

            
        }
    }
}
