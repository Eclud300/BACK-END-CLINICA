using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinica.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionExpedienteMedico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PresionArterial",
                table: "NotasMedicas");

            migrationBuilder.RenameColumn(
                name: "Tratamiento",
                table: "NotasMedicas",
                newName: "PlanTerapeutico");

            migrationBuilder.RenameColumn(
                name: "Sintomas",
                table: "NotasMedicas",
                newName: "MotivoConsulta");

            migrationBuilder.AddColumn<string>(
                name: "AntecedentesHeredoFamiliares",
                table: "Pacientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AntecedentesNoPatologicos",
                table: "Pacientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AntecedentesPatologicos",
                table: "Pacientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CURP",
                table: "Pacientes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Pacientes",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Pacientes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Pacientes",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "NotasMedicas",
                type: "numeric(5,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodigoCIE10",
                table: "NotasMedicas",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstudiosAuxiliares",
                table: "NotasMedicas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExploracionFisica",
                table: "NotasMedicas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrecuenciaCardiaca",
                table: "NotasMedicas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrecuenciaRespiratoria",
                table: "NotasMedicas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IMC",
                table: "NotasMedicas",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecetaMedicamentos",
                table: "NotasMedicas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaturacionOxigeno",
                table: "NotasMedicas",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Talla",
                table: "NotasMedicas",
                type: "numeric(4,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Temperatura",
                table: "NotasMedicas",
                type: "numeric(4,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AntecedentesHeredoFamiliares",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "AntecedentesNoPatologicos",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "AntecedentesPatologicos",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "CURP",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "CodigoCIE10",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "EstudiosAuxiliares",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "ExploracionFisica",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "FrecuenciaCardiaca",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "FrecuenciaRespiratoria",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "IMC",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "RecetaMedicamentos",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "SaturacionOxigeno",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "Talla",
                table: "NotasMedicas");

            migrationBuilder.DropColumn(
                name: "Temperatura",
                table: "NotasMedicas");

            migrationBuilder.RenameColumn(
                name: "PlanTerapeutico",
                table: "NotasMedicas",
                newName: "Tratamiento");

            migrationBuilder.RenameColumn(
                name: "MotivoConsulta",
                table: "NotasMedicas",
                newName: "Sintomas");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "NotasMedicas",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresionArterial",
                table: "NotasMedicas",
                type: "character varying(15)",
                maxLength: 15,
                nullable: true);
        }
    }
}
