using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_Bosch.Migrations
{
    /// <inheritdoc />
    public partial class auditchange1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "AuditLogs",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AuditLogs",
                newName: "Payload");

            migrationBuilder.AddColumn<string>(
                name: "CorrelationId",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "AuditLogs");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AuditLogs",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Payload",
                table: "AuditLogs",
                newName: "Password");
        }
    }
}
