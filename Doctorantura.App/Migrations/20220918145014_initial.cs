using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctorantura.App.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnNums",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnNums", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LineNums",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false),
                    ColumnNumId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineNums", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineNums_ColumnNums_ColumnNumId",
                        column: x => x.ColumnNumId,
                        principalTable: "ColumnNums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ColumnNums",
                columns: new[] { "ID", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "K1", 1.0 },
                    { 2, "K2", 2.0 },
                    { 3, "K3", 3.0 },
                    { 4, "K4", 3.0 },
                    { 5, "K5", 5.0 },
                    { 6, "K6", 7.0 },
                    { 7, "K7", 9.0 },
                    { 8, "K8", 9.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineNums_ColumnNumId",
                table: "LineNums",
                column: "ColumnNumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineNums");

            migrationBuilder.DropTable(
                name: "ColumnNums");
        }
    }
}
