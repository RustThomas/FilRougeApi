using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilRougeAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModdifDeleteDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprunts_Lecteurs_LecteurId",
                table: "Emprunts");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprunts_Livres_LivreId",
                table: "Emprunts");

            migrationBuilder.DropForeignKey(
                name: "FK_Livres_Auteurs_AuteurId",
                table: "Livres");

            migrationBuilder.DropForeignKey(
                name: "FK_Livres_Domaines_DomaineId",
                table: "Livres");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprunts_Lecteurs_LecteurId",
                table: "Emprunts",
                column: "LecteurId",
                principalTable: "Lecteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprunts_Livres_LivreId",
                table: "Emprunts",
                column: "LivreId",
                principalTable: "Livres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livres_Auteurs_AuteurId",
                table: "Livres",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Livres_Domaines_DomaineId",
                table: "Livres",
                column: "DomaineId",
                principalTable: "Domaines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprunts_Lecteurs_LecteurId",
                table: "Emprunts");

            migrationBuilder.DropForeignKey(
                name: "FK_Emprunts_Livres_LivreId",
                table: "Emprunts");

            migrationBuilder.DropForeignKey(
                name: "FK_Livres_Auteurs_AuteurId",
                table: "Livres");

            migrationBuilder.DropForeignKey(
                name: "FK_Livres_Domaines_DomaineId",
                table: "Livres");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprunts_Lecteurs_LecteurId",
                table: "Emprunts",
                column: "LecteurId",
                principalTable: "Lecteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprunts_Livres_LivreId",
                table: "Emprunts",
                column: "LivreId",
                principalTable: "Livres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livres_Auteurs_AuteurId",
                table: "Livres",
                column: "AuteurId",
                principalTable: "Auteurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Livres_Domaines_DomaineId",
                table: "Livres",
                column: "DomaineId",
                principalTable: "Domaines",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
