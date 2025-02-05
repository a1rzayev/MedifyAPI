using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedifyAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DoctorValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserValidations",
                table: "UserValidations");

            migrationBuilder.RenameTable(
                name: "UserValidations",
                newName: "UserValidation");

            migrationBuilder.AlterColumn<string>(
                name: "WorkDaysHours",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserValidation",
                table: "UserValidation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserValidation",
                table: "UserValidation");

            migrationBuilder.RenameTable(
                name: "UserValidation",
                newName: "UserValidations");

            migrationBuilder.AlterColumn<string>(
                name: "WorkDaysHours",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserValidations",
                table: "UserValidations",
                column: "UserId");
        }
    }
}
