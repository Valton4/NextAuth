using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextAuth.Migrations
{
    public partial class fux : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorPublicationDetails_ProfessorPublicationForms_PublicationFormId",
                table: "ProfessorPublicationDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorPublicationDetails_ProfessorPublications_PublicationId",
                table: "ProfessorPublicationDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublicationTypes",
                table: "ProfessorPublicationTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublications",
                table: "ProfessorPublications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublicationForms",
                table: "ProfessorPublicationForms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublicationDetails",
                table: "ProfessorPublicationDetails");

            migrationBuilder.RenameTable(
                name: "ProfessorPublicationTypes",
                newName: "ProfessorPublicationType");

            migrationBuilder.RenameTable(
                name: "ProfessorPublications",
                newName: "ProfessorPublication");

            migrationBuilder.RenameTable(
                name: "ProfessorPublicationForms",
                newName: "ProfessorPublicationForm");

            migrationBuilder.RenameTable(
                name: "ProfessorPublicationDetails",
                newName: "ProfessorPublicationDetail");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorPublicationDetails_PublicationId",
                table: "ProfessorPublicationDetail",
                newName: "IX_ProfessorPublicationDetail_PublicationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorPublicationDetails_PublicationFormId",
                table: "ProfessorPublicationDetail",
                newName: "IX_ProfessorPublicationDetail_PublicationFormId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProfessorPublicationType",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProfessorPublication",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProfessorPublication",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProfessorPublicationForm",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Field",
                table: "ProfessorPublicationForm",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublicationType",
                table: "ProfessorPublicationType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublication",
                table: "ProfessorPublication",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublicationForm",
                table: "ProfessorPublicationForm",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublicationDetail",
                table: "ProfessorPublicationDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Professor__Publi__1D114BD1",
                table: "ProfessorPublicationDetail",
                column: "PublicationFormId",
                principalTable: "ProfessorPublicationForm",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Professor__Publi__25A691D2",
                table: "ProfessorPublicationDetail",
                column: "PublicationId",
                principalTable: "ProfessorPublication",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Professor__Publi__1D114BD1",
                table: "ProfessorPublicationDetail");

            migrationBuilder.DropForeignKey(
                name: "FK__Professor__Publi__25A691D2",
                table: "ProfessorPublicationDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublicationType",
                table: "ProfessorPublicationType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublicationForm",
                table: "ProfessorPublicationForm");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublicationDetail",
                table: "ProfessorPublicationDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorPublication",
                table: "ProfessorPublication");

            migrationBuilder.RenameTable(
                name: "ProfessorPublicationType",
                newName: "ProfessorPublicationTypes");

            migrationBuilder.RenameTable(
                name: "ProfessorPublicationForm",
                newName: "ProfessorPublicationForms");

            migrationBuilder.RenameTable(
                name: "ProfessorPublicationDetail",
                newName: "ProfessorPublicationDetails");

            migrationBuilder.RenameTable(
                name: "ProfessorPublication",
                newName: "ProfessorPublications");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorPublicationDetail_PublicationId",
                table: "ProfessorPublicationDetails",
                newName: "IX_ProfessorPublicationDetails_PublicationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorPublicationDetail_PublicationFormId",
                table: "ProfessorPublicationDetails",
                newName: "IX_ProfessorPublicationDetails_PublicationFormId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProfessorPublicationTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProfessorPublicationForms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Field",
                table: "ProfessorPublicationForms",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProfessorPublications",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ProfessorPublications",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublicationTypes",
                table: "ProfessorPublicationTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublicationForms",
                table: "ProfessorPublicationForms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublicationDetails",
                table: "ProfessorPublicationDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorPublications",
                table: "ProfessorPublications",
                column: "Id");

            
            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorPublicationDetails_ProfessorPublicationForms_PublicationFormId",
                table: "ProfessorPublicationDetails",
                column: "PublicationFormId",
                principalTable: "ProfessorPublicationForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorPublicationDetails_ProfessorPublications_PublicationId",
                table: "ProfessorPublicationDetails",
                column: "PublicationId",
                principalTable: "ProfessorPublications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
