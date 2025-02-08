using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Grupo27.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HealthMed05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdHorarioMedico",
                table: "HorariosMedicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdHorario",
                table: "HorariosMedicos",
                newName: "IdHorarioMedico");
        }
    }
}
