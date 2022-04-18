using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MWTDbContext.Migrations
{
    public partial class U_TUserDetail_AddressMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detailsMasters");

            migrationBuilder.CreateTable(
                name: "addressMasters",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    Address1 = table.Column<string>(maxLength: 250, nullable: false),
                    Address2 = table.Column<string>(maxLength: 250, nullable: true),
                    Pincode = table.Column<string>(maxLength: 33, nullable: false),
                    createdAt = table.Column<DateTime>(nullable: false),
                    updatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addressMasters", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addressMasters");

            migrationBuilder.CreateTable(
                name: "detailsMasters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    GSTIN = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(33)", maxLength: 33, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailsMasters", x => x.id);
                });
        }
    }
}
