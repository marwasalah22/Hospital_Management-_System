
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management__System.Migrations
{
    public partial class AddMedicalRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "MedicalRecords",
              columns: table => new
              {
                  MedicalRecordId = table.Column<int>(type: "int", nullable: false)
                      .Annotation("SqlServer:Identity", "1, 1"),

                  PatiantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  PatiantEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  PatiantAge = table.Column<int>(type: "int", nullable: false),
                  PatiantAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  PatiantPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Appointment = table.Column<DateTime>(type: "datetime2", nullable: false),
                  IsFinished = table.Column<bool>(type: "bit", nullable: false),
                  DoctorId = table.Column<int>(type: "int", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_MedicalRecords", x => x.MedicalRecordId);
                  table.ForeignKey(
                      name: "FK_MedicalRecords_Doctors_DoctorId",
                      column: x => x.DoctorId,
                      principalTable: "Doctors",
                      principalColumn: "DoctorId",
                      onDelete: ReferentialAction.Restrict);
              });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_DoctorId",
                table: "MedicalRecords",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalRecords");


        }
    }
}
