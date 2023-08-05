using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextAuth.Migrations
{
    public partial class professorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessorId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
                 }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "AspNetUsers");
                }
    }
}
