using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolCatalog.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clase",
                columns: table => new
                {
                    IdClasa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeClasa = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProfilClasa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdOrar = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => x.IdClasa);
                });

            migrationBuilder.CreateTable(
                name: "Profesori",
                columns: table => new
                {
                    IdProfesor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeProfesor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrenumeProfesor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailProfesor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNasterii = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesori", x => x.IdProfesor);
                });

            migrationBuilder.CreateTable(
                name: "Elevi",
                columns: table => new
                {
                    IdElev = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeElev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PrenumeElev = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DataNasterii = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClasaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elevi", x => x.IdElev);
                    table.ForeignKey(
                        name: "FK_Elevi_Clase_ClasaId",
                        column: x => x.ClasaId,
                        principalTable: "Clase",
                        principalColumn: "IdClasa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orare",
                columns: table => new
                {
                    IdOrar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescriereOrar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnScolar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClasa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orare", x => x.IdOrar);
                    table.ForeignKey(
                        name: "FK_Orare_Clase_IdClasa",
                        column: x => x.IdClasa,
                        principalTable: "Clase",
                        principalColumn: "IdClasa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClasaProfesor",
                columns: table => new
                {
                    ClaseIdClasa = table.Column<int>(type: "int", nullable: false),
                    ProfesoriIdProfesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasaProfesor", x => new { x.ClaseIdClasa, x.ProfesoriIdProfesor });
                    table.ForeignKey(
                        name: "FK_ClasaProfesor_Clase_ClaseIdClasa",
                        column: x => x.ClaseIdClasa,
                        principalTable: "Clase",
                        principalColumn: "IdClasa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasaProfesor_Profesori_ProfesoriIdProfesor",
                        column: x => x.ProfesoriIdProfesor,
                        principalTable: "Profesori",
                        principalColumn: "IdProfesor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materii",
                columns: table => new
                {
                    IdMaterie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeMaterie = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materii", x => x.IdMaterie);
                    table.ForeignKey(
                        name: "FK_Materii_Profesori_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesori",
                        principalColumn: "IdProfesor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdElev = table.Column<int>(type: "int", nullable: true),
                    IdProfesor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_Elevi_IdElev",
                        column: x => x.IdElev,
                        principalTable: "Elevi",
                        principalColumn: "IdElev");
                    table.ForeignKey(
                        name: "FK_Users_Profesori_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Profesori",
                        principalColumn: "IdProfesor");
                });

            migrationBuilder.CreateTable(
                name: "ClasaMaterie",
                columns: table => new
                {
                    ClaseIdClasa = table.Column<int>(type: "int", nullable: false),
                    MateriiIdMaterie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClasaMaterie", x => new { x.ClaseIdClasa, x.MateriiIdMaterie });
                    table.ForeignKey(
                        name: "FK_ClasaMaterie_Clase_ClaseIdClasa",
                        column: x => x.ClaseIdClasa,
                        principalTable: "Clase",
                        principalColumn: "IdClasa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClasaMaterie_Materii_MateriiIdMaterie",
                        column: x => x.MateriiIdMaterie,
                        principalTable: "Materii",
                        principalColumn: "IdMaterie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medii",
                columns: table => new
                {
                    IdMedie = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdElev = table.Column<int>(type: "int", nullable: false),
                    IdMaterie = table.Column<int>(type: "int", nullable: false),
                    Valoare = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medii", x => x.IdMedie);
                    table.ForeignKey(
                        name: "FK_Medii_Elevi_IdElev",
                        column: x => x.IdElev,
                        principalTable: "Elevi",
                        principalColumn: "IdElev",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medii_Materii_IdMaterie",
                        column: x => x.IdMaterie,
                        principalTable: "Materii",
                        principalColumn: "IdMaterie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valoare = table.Column<int>(type: "int", nullable: false),
                    DataNotei = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsteAnulata = table.Column<bool>(type: "bit", nullable: false),
                    IdElev = table.Column<int>(type: "int", nullable: false),
                    IdMaterie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.IdNota);
                    table.ForeignKey(
                        name: "FK_Note_Elevi_IdElev",
                        column: x => x.IdElev,
                        principalTable: "Elevi",
                        principalColumn: "IdElev",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_Materii_IdMaterie",
                        column: x => x.IdMaterie,
                        principalTable: "Materii",
                        principalColumn: "IdMaterie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrarItems",
                columns: table => new
                {
                    IdOrarItem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZiSaptamana = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    OraInceput = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    OraSfarsit = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    IdMaterie = table.Column<int>(type: "int", nullable: false),
                    IdProfesor = table.Column<int>(type: "int", nullable: false),
                    IdOrar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrarItems", x => x.IdOrarItem);
                    table.ForeignKey(
                        name: "FK_OrarItems_Materii_IdMaterie",
                        column: x => x.IdMaterie,
                        principalTable: "Materii",
                        principalColumn: "IdMaterie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrarItems_Orare_IdOrar",
                        column: x => x.IdOrar,
                        principalTable: "Orare",
                        principalColumn: "IdOrar",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrarItems_Profesori_IdProfesor",
                        column: x => x.IdProfesor,
                        principalTable: "Profesori",
                        principalColumn: "IdProfesor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teme",
                columns: table => new
                {
                    IdTema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descriere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermenLimita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdMaterie = table.Column<int>(type: "int", nullable: false),
                    IdClasa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teme", x => x.IdTema);
                    table.ForeignKey(
                        name: "FK_Teme_Clase_IdClasa",
                        column: x => x.IdClasa,
                        principalTable: "Clase",
                        principalColumn: "IdClasa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teme_Materii_IdMaterie",
                        column: x => x.IdMaterie,
                        principalTable: "Materii",
                        principalColumn: "IdMaterie",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FisiereTema",
                columns: table => new
                {
                    IdFisier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeFisier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UrlFisier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdElev = table.Column<int>(type: "int", nullable: false),
                    TemaId = table.Column<int>(type: "int", nullable: false),
                    DataIncarcare = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FisiereTema", x => x.IdFisier);
                    table.ForeignKey(
                        name: "FK_FisiereTema_Elevi_IdElev",
                        column: x => x.IdElev,
                        principalTable: "Elevi",
                        principalColumn: "IdElev",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FisiereTema_Teme_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Teme",
                        principalColumn: "IdTema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClasaMaterie_MateriiIdMaterie",
                table: "ClasaMaterie",
                column: "MateriiIdMaterie");

            migrationBuilder.CreateIndex(
                name: "IX_ClasaProfesor_ProfesoriIdProfesor",
                table: "ClasaProfesor",
                column: "ProfesoriIdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Elevi_ClasaId",
                table: "Elevi",
                column: "ClasaId");

            migrationBuilder.CreateIndex(
                name: "IX_FisiereTema_IdElev",
                table: "FisiereTema",
                column: "IdElev");

            migrationBuilder.CreateIndex(
                name: "IX_FisiereTema_TemaId",
                table: "FisiereTema",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materii_ProfesorId",
                table: "Materii",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medii_IdElev",
                table: "Medii",
                column: "IdElev");

            migrationBuilder.CreateIndex(
                name: "IX_Medii_IdMaterie",
                table: "Medii",
                column: "IdMaterie");

            migrationBuilder.CreateIndex(
                name: "IX_Note_IdElev",
                table: "Note",
                column: "IdElev");

            migrationBuilder.CreateIndex(
                name: "IX_Note_IdMaterie",
                table: "Note",
                column: "IdMaterie");

            migrationBuilder.CreateIndex(
                name: "IX_Orare_IdClasa",
                table: "Orare",
                column: "IdClasa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrarItems_IdMaterie",
                table: "OrarItems",
                column: "IdMaterie");

            migrationBuilder.CreateIndex(
                name: "IX_OrarItems_IdOrar",
                table: "OrarItems",
                column: "IdOrar");

            migrationBuilder.CreateIndex(
                name: "IX_OrarItems_IdProfesor",
                table: "OrarItems",
                column: "IdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_Teme_IdClasa",
                table: "Teme",
                column: "IdClasa");

            migrationBuilder.CreateIndex(
                name: "IX_Teme_IdMaterie",
                table: "Teme",
                column: "IdMaterie");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdElev",
                table: "Users",
                column: "IdElev");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdProfesor",
                table: "Users",
                column: "IdProfesor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClasaMaterie");

            migrationBuilder.DropTable(
                name: "ClasaProfesor");

            migrationBuilder.DropTable(
                name: "FisiereTema");

            migrationBuilder.DropTable(
                name: "Medii");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "OrarItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Teme");

            migrationBuilder.DropTable(
                name: "Orare");

            migrationBuilder.DropTable(
                name: "Elevi");

            migrationBuilder.DropTable(
                name: "Materii");

            migrationBuilder.DropTable(
                name: "Clase");

            migrationBuilder.DropTable(
                name: "Profesori");
        }
    }
}
