using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenderType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumberType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumberType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelativeImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderTypeId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_GenderType_GenderTypeId",
                        column: x => x.GenderTypeId,
                        principalTable: "GenderType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumberTypeId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneNumber", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhoneNumber_PhoneNumberType_PhoneNumberTypeId",
                        column: x => x.PhoneNumberTypeId,
                        principalTable: "PhoneNumberType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelationTypeId = table.Column<int>(type: "int", nullable: false),
                    PersonForId = table.Column<int>(type: "int", nullable: false),
                    PersonToId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelatedPerson_Person_PersonForId",
                        column: x => x.PersonForId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelatedPerson_Person_PersonToId",
                        column: x => x.PersonToId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedPerson_RelationType_RelationTypeId",
                        column: x => x.RelationTypeId,
                        principalTable: "RelationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "City",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenderType_Name",
                table: "GenderType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_BirthDate",
                table: "Person",
                column: "BirthDate",
                unique: true,
                filter: "[BirthDate] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Person_CityId",
                table: "Person",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_GenderTypeId",
                table: "Person",
                column: "GenderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonalNumber",
                table: "Person",
                column: "PersonalNumber",
                unique: true,
                filter: "[PersonalNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PersonId",
                table: "PhoneNumber",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_PhoneNumberTypeId",
                table: "PhoneNumber",
                column: "PhoneNumberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_Value",
                table: "PhoneNumber",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumberType_Name",
                table: "PhoneNumberType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPerson_PersonForId_PersonToId",
                table: "RelatedPerson",
                columns: new[] { "PersonForId", "PersonToId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPerson_PersonToId",
                table: "RelatedPerson",
                column: "PersonToId");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPerson_RelationTypeId",
                table: "RelatedPerson",
                column: "RelationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationType_Name",
                table: "RelationType",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "RelatedPerson");

            migrationBuilder.DropTable(
                name: "PhoneNumberType");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "RelationType");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "GenderType");
        }
    }
}
