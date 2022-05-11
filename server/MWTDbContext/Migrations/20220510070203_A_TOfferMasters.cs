using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MWTDbContext.Migrations
{
    public partial class A_TOfferMasters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Offer",
                table: "stockMasters");

            migrationBuilder.DropColumn(
                name: "OfferEnd",
                table: "stockMasters");

            migrationBuilder.DropColumn(
                name: "OfferStart",
                table: "stockMasters");

            migrationBuilder.CreateTable(
                name: "offerMasters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(nullable: false),
                    Offer = table.Column<int>(nullable: false),
                    OfferStart = table.Column<DateTime>(nullable: false),
                    OfferEnd = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offerMasters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offerMasters");

            migrationBuilder.AddColumn<int>(
                name: "Offer",
                table: "stockMasters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferEnd",
                table: "stockMasters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferStart",
                table: "stockMasters",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
