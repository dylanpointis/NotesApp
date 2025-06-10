using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesAppBackend.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    IdNote = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TextContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.IdNote);
                });

            migrationBuilder.CreateTable(
                name: "CategoryNote",
                columns: table => new
                {
                    CategoriesIdCategory = table.Column<int>(type: "int", nullable: false),
                    NotesIdNote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryNote", x => new { x.CategoriesIdCategory, x.NotesIdNote });
                    table.ForeignKey(
                        name: "FK_CategoryNote_Categories_CategoriesIdCategory",
                        column: x => x.CategoriesIdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryNote_Notes_NotesIdNote",
                        column: x => x.NotesIdNote,
                        principalTable: "Notes",
                        principalColumn: "IdNote",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryNote_NotesIdNote",
                table: "CategoryNote",
                column: "NotesIdNote");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryNote");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
