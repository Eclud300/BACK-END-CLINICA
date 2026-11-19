using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Clinica.Migrations
{
    /// <inheritdoc />
    public partial class FusionAdminYClinica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NotasMedicas_CitaId",
                table: "NotasMedicas");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Usuarios",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Activo");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Insumos",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "Óptimo");

            migrationBuilder.AddColumn<string>(
                name: "Folio",
                table: "Citas",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clinicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Logo = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Telefono = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_CURP",
                table: "Pacientes",
                column: "CURP",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotasMedicas_CitaId",
                table: "NotasMedicas",
                column: "CitaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_Folio",
                table: "Citas",
                column: "Folio",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clinicas");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_CURP",
                table: "Pacientes");

            migrationBuilder.DropIndex(
                name: "IX_NotasMedicas_CitaId",
                table: "NotasMedicas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_Folio",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Insumos");

            migrationBuilder.DropColumn(
                name: "Folio",
                table: "Citas");

            migrationBuilder.CreateIndex(
                name: "IX_NotasMedicas_CitaId",
                table: "NotasMedicas",
                column: "CitaId");
        }
    }
}
