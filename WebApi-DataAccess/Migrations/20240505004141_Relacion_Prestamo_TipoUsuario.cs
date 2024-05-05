using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Relacion_Prestamo_TipoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoUsuarioId",
                table: "Prestamos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_TipoUsuarioId",
                table: "Prestamos",
                column: "TipoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_TipoUsuarios_TipoUsuarioId",
                table: "Prestamos",
                column: "TipoUsuarioId",
                principalTable: "TipoUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_TipoUsuarios_TipoUsuarioId",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_TipoUsuarioId",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "TipoUsuarioId",
                table: "Prestamos");
        }
    }
}
