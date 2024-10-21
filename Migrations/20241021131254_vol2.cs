using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatAPI.Migrations
{
    /// <inheritdoc />
    public partial class vol2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cats_CatId",
                table: "Cats");

            migrationBuilder.AlterColumn<string>(
                name: "CatId",
                table: "Cats",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CatId",
                table: "Cats",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_CatId",
                table: "Cats",
                column: "CatId",
                unique: true);
        }
    }
}
