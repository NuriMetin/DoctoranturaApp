using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctorantura.App.Migrations
{
    public partial class finit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Row = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Row = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ColumnsLines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(nullable: false),
                    ColumnId = table.Column<int>(nullable: false),
                    LineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnsLines", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ColumnsLines_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnsLines_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Columns",
                columns: new[] { "ID", "Name", "Row" },
                values: new object[,]
                {
                    { 1, "K1", 1 },
                    { 2, "K2", 2 },
                    { 3, "K3", 3 },
                    { 4, "K4", 4 },
                    { 5, "K5", 5 },
                    { 6, "K6", 6 },
                    { 7, "K7", 7 },
                    { 8, "K8", 8 }
                });

            migrationBuilder.InsertData(
                table: "Lines",
                columns: new[] { "ID", "Name", "Row" },
                values: new object[,]
                {
                    { 1, "K1", 1 },
                    { 2, "K2", 2 },
                    { 3, "K3", 3 },
                    { 4, "K4", 4 },
                    { 5, "K5", 5 },
                    { 6, "K6", 6 },
                    { 7, "K7", 7 },
                    { 8, "K8", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnsLines_ColumnId",
                table: "ColumnsLines",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnsLines_LineId",
                table: "ColumnsLines",
                column: "LineId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnsLines");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Lines");
        }
    }
}
