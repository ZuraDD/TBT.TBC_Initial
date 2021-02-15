using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RelatedPersonConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relation_Person_PersonForId",
                table: "Relation");

            migrationBuilder.AddForeignKey(
                name: "FK_Relation_Person_PersonForId",
                table: "Relation",
                column: "PersonForId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relation_Person_PersonForId",
                table: "Relation");

            migrationBuilder.AddForeignKey(
                name: "FK_Relation_Person_PersonForId",
                table: "Relation",
                column: "PersonForId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
