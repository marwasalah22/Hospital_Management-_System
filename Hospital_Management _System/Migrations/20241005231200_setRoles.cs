using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management__System.Migrations
{
    public partial class setRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1962d8fb-92b4-41d3-bf0e-d6929618b859", "75f73d88-abdf-4611-890f-06860321ae58", "Doctor", "doctor" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "561415e4-af39-400c-bd66-22d5e8d1b8b1", "919a78aa-eb19-4e60-b3f9-31681bd63fb7", "Nurse", "nurse" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97bd0162-f960-473a-beb3-a77cbaa5cbdd", "5473c08f-c17d-41c4-8bf4-32dbbc8f91ea", "Patient", "patient" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1962d8fb-92b4-41d3-bf0e-d6929618b859");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "561415e4-af39-400c-bd66-22d5e8d1b8b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97bd0162-f960-473a-beb3-a77cbaa5cbdd");

           
        }
    }
}
