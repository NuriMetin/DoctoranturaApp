using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctorantura.App.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlfLines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineNum = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlfLines", x => x.ID);
                });

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
                name: "QamLines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineNum = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QamLines", x => x.ID);
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
                name: "LinesSum",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineNum = table.Column<int>(nullable: false),
                    TotalSum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinesSum", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WLines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineNum = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WLines", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ColumnsLines",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(nullable: false),
                    ColumnId = table.Column<int>(nullable: false),
                    LineId = table.Column<int>(nullable: false),
                    ColumnNum = table.Column<int>(nullable: false),
                    LineNum = table.Column<int>(nullable: false)
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
                name: "AlfLines");

            migrationBuilder.DropTable(
                name: "ColumnsLines");

            migrationBuilder.DropTable(
                name: "QamLines");

            migrationBuilder.DropTable(
                name: "LinesSum");

            migrationBuilder.DropTable(
                name: "WLines");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Lines");
        }
    }
}
