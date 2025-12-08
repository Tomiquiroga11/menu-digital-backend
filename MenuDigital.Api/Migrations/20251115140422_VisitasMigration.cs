using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuDigital.Api.Migrations
{
    /// <inheritdoc />
    public partial class VisitasMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Visitas",
                table: "Restaurantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visitas",
                table: "Restaurantes");
        }
    }
}
