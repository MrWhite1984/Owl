using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfHub.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipants_Projects_PersonId",
                table: "ProjectParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipants_Projects_ProjectId",
                table: "ProjectParticipants",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectParticipants_Projects_ProjectId",
                table: "ProjectParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectParticipants_Projects_PersonId",
                table: "ProjectParticipants",
                column: "PersonId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
