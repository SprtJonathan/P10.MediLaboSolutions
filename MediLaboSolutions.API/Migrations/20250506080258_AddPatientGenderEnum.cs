using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLaboSolutions.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientGenderEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Genre",
                table: "Patients",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "TINYINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Genre",
                table: "Patients",
                type: "TINYINT",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }
    }
}
