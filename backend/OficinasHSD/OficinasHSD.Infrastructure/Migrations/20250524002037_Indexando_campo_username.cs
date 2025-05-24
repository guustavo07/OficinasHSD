using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OficinasHSD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Indexando_campo_username : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TbUsuario_Username",
                table: "TbUsuario",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbUsuario_Username",
                table: "TbUsuario");
        }
    }
}
