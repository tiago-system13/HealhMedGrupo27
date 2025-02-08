using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Grupo27.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HealthMed03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Pacientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
