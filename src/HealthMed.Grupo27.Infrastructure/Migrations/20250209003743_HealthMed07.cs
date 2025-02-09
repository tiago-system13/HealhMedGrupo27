using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Grupo27.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HealthMed07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint("PK_HorariosMedicos_1","HorariosMedicos");
            migrationBuilder.DropColumn("IdHorario", "HorariosMedicos");

            migrationBuilder.AddColumn<int>(
                name: "IdHorario",
                table: "HorariosMedicos",
                type: "int",
                nullable: false,
                defaultValue: 0
                ).Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "IdMedico",
            table: "Medicos",
            nullable: false)
            .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
