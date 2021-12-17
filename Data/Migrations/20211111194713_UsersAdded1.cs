using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UsersAdded1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Brands",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.Sql(@"INSERT INTO dbo.Users (username, email, password, type)
                VALUES ('admin', 'brandsguideadmin@protonmail.com', '1000.uk4pmoMuQYNePDwnBcaGZA==.BdUoQfwkuOUtxBni0ruSJofraqqR0PZ+EQgGUYz5L38=', '1')");

            migrationBuilder.Sql(@"UPDATE dbo.Brands SET CreatorId = 1
                where CreatorId IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatorId",
                table: "Brands",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Users_CreatorId",
                table: "Brands",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Users_CreatorId",
                table: "Brands");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CreatorId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Brands");
        }
    }
}
