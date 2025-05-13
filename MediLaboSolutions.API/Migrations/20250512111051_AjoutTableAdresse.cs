using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLaboSolutions.API.Migrations
{
    /// <inheritdoc />
    public partial class AjoutTableAdresse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Adresses_AdresseId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "Voie",
                table: "Adresses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Adresses_AdresseId",
                table: "Patients",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Adresses_AdresseId",
                table: "Patients");

            migrationBuilder.AlterColumn<string>(
                name: "Voie",
                table: "Adresses",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Adresses_AdresseId",
                table: "Patients",
                column: "AdresseId",
                principalTable: "Adresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
