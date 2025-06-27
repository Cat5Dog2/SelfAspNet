using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SelfAspNet.Migrations
{
    /// <inheritdoc />
    public partial class AuthorsUsersReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NeedNews = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Authors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    BooksId = table.Column<int>(type: "int", nullable: false),
                    AuthorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.BooksId, x.AuthorsId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Isbn", "Price", "Published", "Publisher", "Sample", "Title" },
                values: new object[,]
                {
                    { 1, "978-4-7981-8094-6", 3960, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "翔泳社", true, "独習Java" },
                    { 2, "978-4-7981-7613-0", 3135, new DateTime(2023, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "翔泳社", true, "Androidアプリ開発の教科書" },
                    { 3, "978-4-8156-1948-0", 4400, new DateTime(2023, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "SBクリエイティブ", true, "これからはじめるReact実践入門" },
                    { 4, "978-4-296-07070-1", 2420, new DateTime(2023, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "日経BP", false, "作って学べるHTML＋JavaScriptの基本" },
                    { 5, "978-4-297-13685-7", 3520, new DateTime(2023, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "技術評論社", false, "Nuxt 3 フロントエンド開発の教科書" },
                    { 6, "978-4-297-13288-0", 3520, new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "技術評論社", true, "JavaScript本格入門" },
                    { 7, "978-4-627-85711-7", 2970, new DateTime(2023, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "森北出版", false, "Pythonでできる! 株価データ分析" },
                    { 8, "978-4-7981-7556-0", 4180, new DateTime(2022, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "翔泳社", true, "独習C#" },
                    { 9, "978-4-8156-1336-5", 3740, new DateTime(2022, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "SBクリエイティブ", true, "これからはじめるVue.js 3実践入門" },
                    { 10, "978-4-296-08014-4", 3190, new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "日経BP", false, "基礎からしっかり学ぶC#の教科書" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birth", "Email", "EmailConfirmd", "Name", "NeedNews" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "yyamada@example.com", null, "山田祥寛", false },
                    { 2, new DateTime(1970, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "nyamauchi@example.com", null, "山内直", false },
                    { 3, new DateTime(1990, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "hsuzuki@example.com", null, "鈴木花子", false },
                    { 4, new DateTime(1992, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "tsato@example.com", null, "佐藤太郎", false },
                    { 5, new DateTime(1985, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "kkatafuchi@example.com", null, "片渕彼富", false },
                    { 6, new DateTime(1974, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ssaito@example.com", null, "齊藤新三", false },
                    { 7, new DateTime(2000, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "shanako@example.com", null, "鈴木花子", false },
                    { 8, new DateTime(1978, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ktakae@example.com", null, "高江賢", false }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "PenName", "UserId" },
                values: new object[,]
                {
                    { 1, "WINGS", 1 },
                    { 2, "Sarva", 6 },
                    { 3, "たまデジ。", 2 },
                    { 4, "Canon", 5 },
                    { 5, "Papier", 8 },
                    { 6, "はな", 3 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "BookId", "LastUpdated", "Name" },
                values: new object[,]
                {
                    { 1, "がっつり学習したい人には、うってつけの本です。前半は、キッチリ基礎固めを行い、後半は応用的な内容に。図や用例、構文も多く、辞書代わりにも使えます。", 1, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "山田太郎" },
                    { 2, "環境構築から書かれており初心者から使えますが、分量から見てもわかるように、サラッとは終わらず、説明が深いです。特に「エキスパートに訊く」という読み物が面白い。", 1, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "佐藤花子" },
                    { 3, "コツコツ、独りでじっくり勉強できます。オブジェクト指向プログラミングに紙数が割いてあって、以前、挫折したけど、今回は理解できました。1回目は読み飛ばしたところもあるので、また読み返したいです。", 1, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "鈴木次郎" },
                    { 4, "Androidはすぐにバージョンが上がるので、本を買うのもためらわれるが、この本は、基礎部分がしっかり説明されているので、はじめの一冊にお勧めできます。", 2, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "井上裕子" },
                    { 5, "同様の構成で、Kotlin対応とJava対応があるので、両方手元にあると比較しながら勉強できます。", 2, new DateTime(2023, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "田中健太" },
                    { 6, "テスト、TypeScriptの活用、Next.jsの活用などなど。実践的な内容盛りだくさんで、かなりのボリュームです。サンプルも多く、ダウンロードして実際に動かしながら確認できて、理解が深まります。", 2, new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "藤井由美" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 6, 1 },
                    { 2, 2 },
                    { 1, 3 },
                    { 3, 4 },
                    { 2, 5 },
                    { 1, 6 },
                    { 4, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 5, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_AuthorsId",
                table: "AuthorBook",
                column: "AuthorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_UserId",
                table: "Authors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
