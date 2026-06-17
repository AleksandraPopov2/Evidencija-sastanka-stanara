using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidencijaSastanka.Podaci.Migrations
{
    /// <inheritdoc />
    public partial class PocetnaMigracija : Migration
    {
        /// <inheritdoc /> 
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnickoIme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zgrada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zgrada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sastanak",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumSastanka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojPrisutnih = table.Column<int>(type: "int", nullable: false),
                    ProcenatPrisutnih = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusSastanka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zakljucak = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZgradaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sastanak", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sastanak_Zgrada_ZgradaId",
                        column: x => x.ZgradaId,
                        principalTable: "Zgrada",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stanar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojStana = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZgradaId = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stanar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stanar_Zgrada_ZgradaId",
                        column: x => x.ZgradaId,
                        principalTable: "Zgrada",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PrisustvoNaSastanku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SastanakId = table.Column<int>(type: "int", nullable: false),
                    StanarId = table.Column<int>(type: "int", nullable: false),
                    Prisutan = table.Column<bool>(type: "bit", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrisustvoNaSastanku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrisustvoNaSastanku_Sastanak_SastanakId",
                        column: x => x.SastanakId,
                        principalTable: "Sastanak",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrisustvoNaSastanku_Stanar_StanarId",
                        column: x => x.StanarId,
                        principalTable: "Stanar",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrisustvoNaSastanku_SastanakId",
                table: "PrisustvoNaSastanku",
                column: "SastanakId");

            migrationBuilder.CreateIndex(
                name: "IX_PrisustvoNaSastanku_StanarId",
                table: "PrisustvoNaSastanku",
                column: "StanarId");

            migrationBuilder.CreateIndex(
                name: "IX_Sastanak_ZgradaId",
                table: "Sastanak",
                column: "ZgradaId");

            migrationBuilder.CreateIndex(
                name: "IX_Stanar_ZgradaId",
                table: "Stanar",
                column: "ZgradaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "PrisustvoNaSastanku");

            migrationBuilder.DropTable(
                name: "Sastanak");

            migrationBuilder.DropTable(
                name: "Stanar");

            migrationBuilder.DropTable(
                name: "Zgrada");
        }
    }
}
