using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDatosPedidoRestaurante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoraApertura",
                table: "Restaurantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoraCierre",
                table: "Restaurantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefono",
                table: "Restaurantes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraApertura",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "HoraCierre",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "Telefono",
                table: "Restaurantes");
        }
    }
}
