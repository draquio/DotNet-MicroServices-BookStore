using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorService.Migrations
{
    /// <inheritdoc />
    public partial class RenameId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "BookAuthors",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BookAuthors",
                newName: "id");
        }
    }
}
