using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediLaboSolutions.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePhoneToLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Telephone",
                table: "Patients",
                type: "bigint",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Telephone",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
