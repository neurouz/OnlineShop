using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassLibrary.Migrations
{
    public partial class pcmig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kategorije",
                columns: table => new
                {
                    KategorijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKategorije = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategorije", x => x.KategorijaID);
                });

            migrationBuilder.CreateTable(
                name: "oglasi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naslov = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    BrojPozicija = table.Column<int>(nullable: false),
                    Lokacija = table.Column<string>(nullable: true),
                    DatumObjave = table.Column<DateTime>(nullable: false),
                    Trajanje = table.Column<int>(nullable: false, defaultValue: 0),
                    DatumIsteka = table.Column<DateTime>(nullable: false),
                    Aktivan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oglasi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "uvoznici",
                columns: table => new
                {
                    UvoznikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivUvoznika = table.Column<string>(nullable: true),
                    AdresaUvoznika = table.Column<string>(nullable: true),
                    BrojTelefona = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uvoznici", x => x.UvoznikID);
                });

            migrationBuilder.CreateTable(
                name: "korisnici",
                columns: table => new
                {
                    KorisnikID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    BrojTelefona = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DatumRegistracije = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    OglasId = table.Column<int>(nullable: true),
                    Nesto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_korisnici", x => x.KorisnikID);
                    table.ForeignKey(
                        name: "FK_korisnici_oglasi_OglasId",
                        column: x => x.OglasId,
                        principalTable: "oglasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "proizvodi",
                columns: table => new
                {
                    ProizvodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivProizvoda = table.Column<string>(nullable: true),
                    Cijena = table.Column<double>(nullable: false),
                    Kolicina = table.Column<int>(nullable: false),
                    OpisProizvoda = table.Column<string>(nullable: true),
                    kategorijaId = table.Column<int>(nullable: false),
                    uvoznikId = table.Column<int>(nullable: false),
                    imageLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proizvodi", x => x.ProizvodID);
                    table.ForeignKey(
                        name: "FK_proizvodi_kategorije_kategorijaId",
                        column: x => x.kategorijaId,
                        principalTable: "kategorije",
                        principalColumn: "KategorijaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_proizvodi_uvoznici_uvoznikId",
                        column: x => x.uvoznikId,
                        principalTable: "uvoznici",
                        principalColumn: "UvoznikID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "recenzije",
                columns: table => new
                {
                    RecenzijaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocjena = table.Column<int>(nullable: false),
                    Komentar = table.Column<string>(nullable: true),
                    ProizvodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recenzije", x => x.RecenzijaID);
                    table.ForeignKey(
                        name: "FK_recenzije_proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "proizvodi",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_korisnici_OglasId",
                table: "korisnici",
                column: "OglasId");

            migrationBuilder.CreateIndex(
                name: "IX_proizvodi_kategorijaId",
                table: "proizvodi",
                column: "kategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_proizvodi_uvoznikId",
                table: "proizvodi",
                column: "uvoznikId");

            migrationBuilder.CreateIndex(
                name: "IX_recenzije_ProizvodId",
                table: "recenzije",
                column: "ProizvodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "korisnici");

            migrationBuilder.DropTable(
                name: "recenzije");

            migrationBuilder.DropTable(
                name: "oglasi");

            migrationBuilder.DropTable(
                name: "proizvodi");

            migrationBuilder.DropTable(
                name: "kategorije");

            migrationBuilder.DropTable(
                name: "uvoznici");
        }
    }
}
