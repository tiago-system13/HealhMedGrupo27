using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthMed.Grupo27.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HealthMed06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("DiaSemana", "HorariosMedicos");

            migrationBuilder.AddColumn<int>(
                name: "DiaSemana",
                table: "HorariosMedicos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DiaSemana",
                table: "HorariosMedicos",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
