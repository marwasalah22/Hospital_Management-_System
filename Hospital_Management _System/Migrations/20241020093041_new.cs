using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management__System.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatiantEmail",
                table: "MedicalRecords",
                newName: "PatiantEmailnew");

            migrationBuilder.AddColumn<string>(
                name: "NurseEmail",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NurseEmail",
                table: "Nurses");

            migrationBuilder.RenameColumn(
                name: "PatiantEmail",
                table: "MedicalRecords",
                newName: "PatientEmailnew");
        }
    }
}
