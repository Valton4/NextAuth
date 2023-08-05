using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextAuth.Migrations
{
    public partial class sss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfessorPublicationForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationTypeId = table.Column<int>(type: "int", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Shown = table.Column<bool>(type: "bit", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationRegex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorPublicationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorPublications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorPublications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorPublicationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorPublicationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorPublicationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationId = table.Column<int>(type: "int", nullable: false),
                    PublicationFormId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorPublicationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessorPublicationDetails_ProfessorPublicationForms_PublicationFormId",
                        column: x => x.PublicationFormId,
                        principalTable: "ProfessorPublicationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorPublicationDetails_ProfessorPublications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "ProfessorPublications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
          
            migrationBuilder.CreateIndex(
                name: "IX_ProfessorPublicationDetails_PublicationFormId",
                table: "ProfessorPublicationDetails",
                column: "PublicationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorPublicationDetails_PublicationId",
                table: "ProfessorPublicationDetails",
                column: "PublicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessorPublicationDetails");

            migrationBuilder.DropTable(
                name: "ProfessorPublicationTypes");

            migrationBuilder.DropTable(
                name: "ProfessorPublicationForms");

            migrationBuilder.DropTable(
                name: "ProfessorPublications");

        }
    }
}
