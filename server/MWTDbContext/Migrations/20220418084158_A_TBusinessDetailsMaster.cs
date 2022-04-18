using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MWTDbContext.Migrations
{
    public partial class A_TBusinessDetailsMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "businessDetailsMasters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    GSTIN = table.Column<string>(maxLength: 16, nullable: false),
                    PAN = table.Column<string>(maxLength: 11, nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businessDetailsMasters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "businessDetailsMasters");
        }
    }
}
