using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MWTDbContext.Migrations
{
    public partial class A_TStockMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stockMasters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Offer = table.Column<int>(nullable: false),
                    OfferStart = table.Column<DateTime>(nullable: false),
                    OfferEnd = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockMasters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stockMasters");
        }
    }
}
