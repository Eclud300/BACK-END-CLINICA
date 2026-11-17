using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Clinica.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Correo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Rol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Estudios = table.Column<string>(type: "text", nullable: true),
                    UniversidadEgreso = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CedulaProfesional = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CedulaEspecialidad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FirmaDigital = table.Column<string>(type: "text", nullable: true),
                    PorcentajeComision = table.Column<decimal>(type: "numeric", nullable: true),
                    TarifaConsulta = table.Column<decimal>(type: "numeric", nullable: true),
                    FechaFirmaPrivacidad = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UltimoLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Requiere2FA = table.Column<bool>(type: "boolean", nullable: false),
                    Secreto2FA = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IntentosFallidos = table.Column<int>(type: "integer", nullable: false),
                    BloqueadoHasta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
