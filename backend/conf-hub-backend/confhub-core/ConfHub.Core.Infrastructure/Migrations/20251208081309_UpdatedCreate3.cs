using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfHub.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Persons_Id",
                table: "News");

            migrationBuilder.CreateIndex(
                name: "IX_News_AuthorPersonId",
                table: "News",
                column: "AuthorPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Persons_AuthorPersonId",
                table: "News",
                column: "AuthorPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Persons_AuthorPersonId",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_News_AuthorPersonId",
                table: "News");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Persons_Id",
                table: "News",
                column: "Id",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
