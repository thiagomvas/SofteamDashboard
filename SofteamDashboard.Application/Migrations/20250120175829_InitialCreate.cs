using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SofteamDashboard.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    GithubUrl = table.Column<string>(type: "TEXT", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Cargo = table.Column<int>(type: "INTEGER", nullable: false),
                    Area = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabilidadeFuncionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FuncionarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    NomeHabilidade = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    Verificado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabilidadeFuncionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabilidadeFuncionarios_Funcionarios_FuncionarioId",
                        column: x => x.FuncionarioId,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_Id",
                table: "Funcionarios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadeFuncionarios_FuncionarioId",
                table: "HabilidadeFuncionarios",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HabilidadeFuncionarios_Id",
                table: "HabilidadeFuncionarios",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabilidadeFuncionarios");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
