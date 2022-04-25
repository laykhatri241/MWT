using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MWTDbContext.Migrations
{
    public partial class A_TProductMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "productMasters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerID = table.Column<int>(nullable: false),
                    ProdCompanyName = table.Column<string>(maxLength: 65, nullable: false),
                    ProdName = table.Column<string>(maxLength: 129, nullable: false),
                    ProdDetails = table.Column<string>(maxLength: 500, nullable: false),
                    ProdImage = table.Column<string>(nullable: true),
                    ProdPrice = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productMasters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productMasters");
        }
    }
}
