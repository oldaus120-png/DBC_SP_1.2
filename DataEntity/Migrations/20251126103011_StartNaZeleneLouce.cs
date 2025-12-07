using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class StartNaZeleneLouce : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notebooky",
                columns: table => new
                {
                    IDNotebooku = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Vyrobce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CPU = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RAM_GB = table.Column<int>(type: "int", nullable: false),
                    KapacitaUloziste_GB = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PocetKusuSkladem = table.Column<int>(type: "int", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notebooky", x => x.IDNotebooku);
                });

            migrationBuilder.CreateTable(
                name: "Uživatel",
                columns: table => new
                {
                    IDUzivatele = table.Column<int>(type: "int", nullable: false),
                    Jmeno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uživatel", x => x.IDUzivatele);
                });

            migrationBuilder.CreateTable(
                name: "Objednavky",
                columns: table => new
                {
                    IDObjednavky = table.Column<int>(type: "int", nullable: false),
                    IDUzivatele = table.Column<int>(type: "int", nullable: false),
                    IDNotebooku = table.Column<int>(type: "int", nullable: false),
                    DatumObjdenavky = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CenaSDPH = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    CenaBezDPH = table.Column<decimal>(type: "decimal(18,2)", maxLength: 100, nullable: false),
                    ICO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objednavky", x => x.IDObjednavky);
                    table.ForeignKey(
                        name: "FK_Objednavky_Notebooky_IDNotebooku",
                        column: x => x.IDNotebooku,
                        principalTable: "Notebooky",
                        principalColumn: "IDNotebooku",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Objednavky_Uživatel_IDUzivatele",
                        column: x => x.IDUzivatele,
                        principalTable: "Uživatel",
                        principalColumn: "IDUzivatele",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Objednavky_IDNotebooku",
                table: "Objednavky",
                column: "IDNotebooku");

            migrationBuilder.CreateIndex(
                name: "IX_Objednavky_IDUzivatele",
                table: "Objednavky",
                column: "IDUzivatele");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objednavky");

            migrationBuilder.DropTable(
                name: "Notebooky");

            migrationBuilder.DropTable(
                name: "Uživatel");
        }
    }
}
