using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLaboSolutions.API.Migrations
{
    /// <inheritdoc />
    public partial class SetAdresseAsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_AdresseId",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AdresseId",
                table: "Patients",
                column: "AdresseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_AdresseId",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AdresseId",
                table: "Patients",
                column: "AdresseId",
                unique: true,
                filter: "[AdresseId] IS NOT NULL");
        }
    }
}
