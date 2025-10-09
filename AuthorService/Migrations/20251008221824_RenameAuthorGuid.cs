using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorService.Migrations
{
    /// <inheritdoc />
    public partial class RenameAuthorGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookAutorGuid",
                table: "Authors");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorGuid",
                table: "Authors",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorGuid",
                table: "Authors");

            migrationBuilder.AddColumn<string>(
                name: "BookAutorGuid",
                table: "Authors",
                type: "text",
                nullable: true);
        }
    }
}
