using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AuthorService.Migrations
{
    /// <inheritdoc />
    public partial class RenameToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegrees_BookAuthors_BookAuthorId",
                table: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "BookAuthors");

            migrationBuilder.RenameColumn(
                name: "BookAuthorId",
                table: "AcademicDegrees",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicDegrees_BookAuthorId",
                table: "AcademicDegrees",
                newName: "IX_AcademicDegrees_AuthorId");

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Lastname = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BookAutorGuid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegrees_Authors_AuthorId",
                table: "AcademicDegrees",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegrees_Authors_AuthorId",
                table: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "AcademicDegrees",
                newName: "BookAuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicDegrees_AuthorId",
                table: "AcademicDegrees",
                newName: "IX_AcademicDegrees_BookAuthorId");

            migrationBuilder.CreateTable(
                name: "BookAuthors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BookAutorGuid = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthors", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegrees_BookAuthors_BookAuthorId",
                table: "AcademicDegrees",
                column: "BookAuthorId",
                principalTable: "BookAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
