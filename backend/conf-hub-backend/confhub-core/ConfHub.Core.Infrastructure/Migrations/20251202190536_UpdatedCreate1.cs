using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfHub.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgramFileUrl",
                table: "Conferences");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sections",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AcceptionFileUrl",
                table: "ProjectParticipants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Conferences",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "AcceptionFileUrl",
                table: "ProjectParticipants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Conferences");

            migrationBuilder.AddColumn<string>(
                name: "ProgramFileUrl",
                table: "Conferences",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
