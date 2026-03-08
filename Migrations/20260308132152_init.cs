using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Monolith.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    region_name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "offer_categories",
                columns: table => new
                {
                    OfferId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offer_categories", x => new { x.OfferId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_offer_categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_offer_categories_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    value = table.Column<decimal>(type: "TEXT", nullable: false),
                    offer_id = table.Column<int>(type: "INTEGER", nullable: false),
                    region_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prices_Offers_offer_id",
                        column: x => x.offer_id,
                        principalTable: "Offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prices_Regions_region_id",
                        column: x => x.region_id,
                        principalTable: "Regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Стеновые материалы" },
                    { 2, "Кровельные материалы" },
                    { 3, "Сухие смеси" },
                    { 4, "Древесные материалы" },
                    { 5, "Крепеж и метизы" },
                    { 6, "Инструменты" },
                    { 7, "Сантехника" },
                    { 8, "Электрика" },
                    { 9, "Отделочные материалы" },
                    { 10, "Утеплители" }
                });

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Кирпич керамический М-150" },
                    { 2, "Блок газосиликатный 600x300x200" },
                    { 3, "Пеноблок D600 600x300x200" },
                    { 4, "Металлочерепица Монтеррей 0.5мм" },
                    { 5, "Профнастил С-8 2м" },
                    { 6, "Гибкая черепица Shinglas" },
                    { 7, "Цемент М500 50кг" },
                    { 8, "Пескобетон М300 40кг" },
                    { 9, "Штукатурка гипсовая 30кг" },
                    { 10, "Клей для плитки 25кг" },
                    { 11, "Брус 100x100x6000" },
                    { 12, "Доска обрезная 50x150x6000" },
                    { 13, "Фанера ФК 1525x1525x12мм" },
                    { 14, "ОСБ плита 2500x1250x9мм" },
                    { 15, "Анкер клиновой 10x100" },
                    { 16, "Саморезы по дереву 5x50 (упак 200шт)" },
                    { 17, "Дюбель-гвоздь 6x40 (упак 100шт)" },
                    { 18, "Болты фундаментные 16x200" },
                    { 19, "Перфоратор Bosch GBH 2-26" },
                    { 20, "Шуруповерт Makita 6281D" },
                    { 21, "Болгарка УШМ 125мм" },
                    { 22, "Лазерный уровень 3 линии" },
                    { 23, "Труба ППР 25x4.2 (3м)" },
                    { 24, "Смеситель для ванны" },
                    { 25, "Унитаз компакт" },
                    { 26, "Раковина керамическая 60см" },
                    { 27, "Кабель ВВГ-нг 3x2.5 (м)" },
                    { 28, "Автомат IEK 16A" },
                    { 29, "Розетка с заземлением" },
                    { 30, "Выключатель одноклавишный" },
                    { 31, "Обои виниловые 10м" },
                    { 32, "Краска акриловая 5л" },
                    { 33, "Плитка керамическая 30x30" },
                    { 34, "Ламинат 32 класс 8мм" },
                    { 35, "Минеральная вата Rockwool 50мм" },
                    { 36, "Пенопласт ПСБ-С 25 1000x1000x50" },
                    { 37, "Эковата (мешок 15кг)" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "id", "region_name" },
                values: new object[,]
                {
                    { 1, "Москва" },
                    { 2, "Санкт-Петербург" },
                    { 3, "Екатеринбург" },
                    { 4, "Казань" },
                    { 5, "Новосибирск" }
                });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "id", "offer_id", "region_id", "value" },
                values: new object[,]
                {
                    { 1, 1, 1, 65m },
                    { 2, 2, 1, 4500m },
                    { 3, 4, 1, 650m },
                    { 4, 7, 1, 550m },
                    { 5, 11, 1, 850m },
                    { 6, 15, 1, 45m },
                    { 7, 19, 1, 8900m },
                    { 8, 23, 1, 350m },
                    { 9, 27, 1, 85m },
                    { 10, 31, 1, 1200m },
                    { 11, 1, 2, 62m },
                    { 12, 2, 2, 4300m },
                    { 13, 4, 2, 620m },
                    { 14, 7, 2, 530m },
                    { 15, 11, 2, 820m },
                    { 16, 19, 2, 8700m },
                    { 17, 27, 2, 82m },
                    { 18, 1, 3, 60m },
                    { 19, 2, 3, 4200m },
                    { 20, 4, 3, 600m },
                    { 21, 7, 3, 520m },
                    { 22, 11, 3, 800m },
                    { 23, 19, 3, 8500m },
                    { 24, 23, 3, 330m },
                    { 25, 27, 3, 80m },
                    { 26, 1, 4, 63m },
                    { 27, 2, 4, 4400m },
                    { 28, 4, 4, 630m },
                    { 29, 7, 4, 540m },
                    { 30, 11, 4, 830m },
                    { 31, 19, 4, 8800m },
                    { 32, 27, 4, 83m },
                    { 33, 1, 5, 61m },
                    { 34, 2, 5, 4250m },
                    { 35, 4, 5, 610m },
                    { 36, 7, 5, 525m },
                    { 37, 11, 5, 810m },
                    { 38, 19, 5, 8600m },
                    { 39, 23, 5, 340m },
                    { 40, 27, 5, 81m }
                });

            migrationBuilder.InsertData(
                table: "offer_categories",
                columns: new[] { "CategoryId", "OfferId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 4, 11 },
                    { 4, 12 },
                    { 4, 13 },
                    { 4, 14 },
                    { 5, 15 },
                    { 5, 16 },
                    { 5, 17 },
                    { 5, 18 },
                    { 6, 19 },
                    { 6, 20 },
                    { 6, 21 },
                    { 6, 22 },
                    { 7, 23 },
                    { 7, 24 },
                    { 7, 25 },
                    { 7, 26 },
                    { 8, 27 },
                    { 8, 28 },
                    { 8, 29 },
                    { 8, 30 },
                    { 9, 31 },
                    { 9, 32 },
                    { 9, 33 },
                    { 9, 34 },
                    { 10, 35 },
                    { 10, 36 },
                    { 10, 37 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_offer_categories_CategoryId",
                table: "offer_categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_offer_id",
                table: "Prices",
                column: "offer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_region_id",
                table: "Prices",
                column: "region_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "offer_categories");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
