using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SelfAspNet.Migrations
{
    /// <inheritdoc />
    public partial class CreateMetasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Metas",
                columns: new[] { "Id", "Action", "Content", "Controller", "Name" },
                values: new object[,]
                {
                    { 1, "Privacy", "メタ情報", "Home", "keywords" },
                    { 2, "Privacy", "ページごとに異なる<meta>要素を生成", "Home", "description" },
                    { 3, "Index", "メタ情報取得の別解", "Tag", "description" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Metas");
        }
    }
}
