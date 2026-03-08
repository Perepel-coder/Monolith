using Microsoft.EntityFrameworkCore;
using Monolith.Applications.Interfaces;
using Monolith.Models;

namespace Monolith;

public partial class ApplicationContext : DbContext, IApplicationDbContext
{
    private static void SetData(ModelBuilder modelBuilder)
    {
        // 1. Регионы (оставляем без изменений)
        modelBuilder.Entity<Region>().HasData(
            new Region { Id = 1, RegionName = "Москва" },
            new Region { Id = 2, RegionName = "Санкт-Петербург" },
            new Region { Id = 3, RegionName = "Екатеринбург" },
            new Region { Id = 4, RegionName = "Казань" },
            new Region { Id = 5, RegionName = "Новосибирск" }
        );

        // 2. Категории строительных материалов
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Стеновые материалы" },
            new Category { Id = 2, Name = "Кровельные материалы" },
            new Category { Id = 3, Name = "Сухие смеси" },
            new Category { Id = 4, Name = "Древесные материалы" },
            new Category { Id = 5, Name = "Крепеж и метизы" },
            new Category { Id = 6, Name = "Инструменты" },
            new Category { Id = 7, Name = "Сантехника" },
            new Category { Id = 8, Name = "Электрика" },
            new Category { Id = 9, Name = "Отделочные материалы" },
            new Category { Id = 10, Name = "Утеплители" }
        );

        // 3. Товары (строительные материалы)
        modelBuilder.Entity<Offer>().HasData(
            // Стеновые материалы
            new Offer { Id = 1, Name = "Кирпич керамический М-150" },
            new Offer { Id = 2, Name = "Блок газосиликатный 600x300x200" },
            new Offer { Id = 3, Name = "Пеноблок D600 600x300x200" },

            // Кровельные материалы
            new Offer { Id = 4, Name = "Металлочерепица Монтеррей 0.5мм" },
            new Offer { Id = 5, Name = "Профнастил С-8 2м" },
            new Offer { Id = 6, Name = "Гибкая черепица Shinglas" },

            // Сухие смеси
            new Offer { Id = 7, Name = "Цемент М500 50кг" },
            new Offer { Id = 8, Name = "Пескобетон М300 40кг" },
            new Offer { Id = 9, Name = "Штукатурка гипсовая 30кг" },
            new Offer { Id = 10, Name = "Клей для плитки 25кг" },

            // Древесные материалы
            new Offer { Id = 11, Name = "Брус 100x100x6000" },
            new Offer { Id = 12, Name = "Доска обрезная 50x150x6000" },
            new Offer { Id = 13, Name = "Фанера ФК 1525x1525x12мм" },
            new Offer { Id = 14, Name = "ОСБ плита 2500x1250x9мм" },

            // Крепеж
            new Offer { Id = 15, Name = "Анкер клиновой 10x100" },
            new Offer { Id = 16, Name = "Саморезы по дереву 5x50 (упак 200шт)" },
            new Offer { Id = 17, Name = "Дюбель-гвоздь 6x40 (упак 100шт)" },
            new Offer { Id = 18, Name = "Болты фундаментные 16x200" },

            // Инструменты
            new Offer { Id = 19, Name = "Перфоратор Bosch GBH 2-26" },
            new Offer { Id = 20, Name = "Шуруповерт Makita 6281D" },
            new Offer { Id = 21, Name = "Болгарка УШМ 125мм" },
            new Offer { Id = 22, Name = "Лазерный уровень 3 линии" },

            // Сантехника
            new Offer { Id = 23, Name = "Труба ППР 25x4.2 (3м)" },
            new Offer { Id = 24, Name = "Смеситель для ванны" },
            new Offer { Id = 25, Name = "Унитаз компакт" },
            new Offer { Id = 26, Name = "Раковина керамическая 60см" },

            // Электрика
            new Offer { Id = 27, Name = "Кабель ВВГ-нг 3x2.5 (м)" },
            new Offer { Id = 28, Name = "Автомат IEK 16A" },
            new Offer { Id = 29, Name = "Розетка с заземлением" },
            new Offer { Id = 30, Name = "Выключатель одноклавишный" },

            // Отделочные материалы
            new Offer { Id = 31, Name = "Обои виниловые 10м" },
            new Offer { Id = 32, Name = "Краска акриловая 5л" },
            new Offer { Id = 33, Name = "Плитка керамическая 30x30" },
            new Offer { Id = 34, Name = "Ламинат 32 класс 8мм" },

            // Утеплители
            new Offer { Id = 35, Name = "Минеральная вата Rockwool 50мм" },
            new Offer { Id = 36, Name = "Пенопласт ПСБ-С 25 1000x1000x50" },
            new Offer { Id = 37, Name = "Эковата (мешок 15кг)" }
        );

        // 4. Связи многие-ко-многим (OfferCategory)
        modelBuilder.Entity("offer_categories").HasData(
            // Стеновые материалы (Category 1)
            new { OfferId = 1, CategoryId = 1 }, // Кирпич
            new { OfferId = 2, CategoryId = 1 }, // Газосиликат
            new { OfferId = 3, CategoryId = 1 }, // Пеноблок

            // Кровельные материалы (Category 2)
            new { OfferId = 4, CategoryId = 2 }, // Металлочерепица
            new { OfferId = 5, CategoryId = 2 }, // Профнастил
            new { OfferId = 6, CategoryId = 2 }, // Гибкая черепица

            // Сухие смеси (Category 3)
            new { OfferId = 7, CategoryId = 3 }, // Цемент
            new { OfferId = 8, CategoryId = 3 }, // Пескобетон
            new { OfferId = 9, CategoryId = 3 }, // Штукатурка
            new { OfferId = 10, CategoryId = 3 }, // Клей

            // Древесные материалы (Category 4)
            new { OfferId = 11, CategoryId = 4 }, // Брус
            new { OfferId = 12, CategoryId = 4 }, // Доска
            new { OfferId = 13, CategoryId = 4 }, // Фанера
            new { OfferId = 14, CategoryId = 4 }, // ОСБ

            // Крепеж (Category 5)
            new { OfferId = 15, CategoryId = 5 }, // Анкер
            new { OfferId = 16, CategoryId = 5 }, // Саморезы
            new { OfferId = 17, CategoryId = 5 }, // Дюбель
            new { OfferId = 18, CategoryId = 5 }, // Болты

            // Инструменты (Category 6)
            new { OfferId = 19, CategoryId = 6 }, // Перфоратор
            new { OfferId = 20, CategoryId = 6 }, // Шуруповерт
            new { OfferId = 21, CategoryId = 6 }, // Болгарка
            new { OfferId = 22, CategoryId = 6 }, // Уровень

            // Сантехника (Category 7)
            new { OfferId = 23, CategoryId = 7 }, // Труба
            new { OfferId = 24, CategoryId = 7 }, // Смеситель
            new { OfferId = 25, CategoryId = 7 }, // Унитаз
            new { OfferId = 26, CategoryId = 7 }, // Раковина

            // Электрика (Category 8)
            new { OfferId = 27, CategoryId = 8 }, // Кабель
            new { OfferId = 28, CategoryId = 8 }, // Автомат
            new { OfferId = 29, CategoryId = 8 }, // Розетка
            new { OfferId = 30, CategoryId = 8 }, // Выключатель

            // Отделочные материалы (Category 9)
            new { OfferId = 31, CategoryId = 9 }, // Обои
            new { OfferId = 32, CategoryId = 9 }, // Краска
            new { OfferId = 33, CategoryId = 9 }, // Плитка
            new { OfferId = 34, CategoryId = 9 }, // Ламинат

            // Утеплители (Category 10)
            new { OfferId = 35, CategoryId = 10 }, // Минвата
            new { OfferId = 36, CategoryId = 10 }, // Пенопласт
            new { OfferId = 37, CategoryId = 10 } // Эковата
        );

        // 5. Цены для разных регионов
        modelBuilder.Entity<Price>().HasData(
            // Москва (RegionId = 1)
            new Price { Id = 1, OfferId = 1, RegionId = 1, Value = 65m }, // Кирпич
            new Price { Id = 2, OfferId = 2, RegionId = 1, Value = 4500m }, // Газосиликат (куб)
            new Price { Id = 3, OfferId = 4, RegionId = 1, Value = 650m }, // Металлочерепица (м2)
            new Price { Id = 4, OfferId = 7, RegionId = 1, Value = 550m }, // Цемент
            new Price { Id = 5, OfferId = 11, RegionId = 1, Value = 850m }, // Брус (шт)
            new Price { Id = 6, OfferId = 15, RegionId = 1, Value = 45m }, // Анкер (шт)
            new Price { Id = 7, OfferId = 19, RegionId = 1, Value = 8900m }, // Перфоратор
            new Price { Id = 8, OfferId = 23, RegionId = 1, Value = 350m }, // Труба
            new Price { Id = 9, OfferId = 27, RegionId = 1, Value = 85m }, // Кабель (м)
            new Price { Id = 10, OfferId = 31, RegionId = 1, Value = 1200m }, // Обои

            // Санкт-Петербург (RegionId = 2) - цены чуть ниже
            new Price { Id = 11, OfferId = 1, RegionId = 2, Value = 62m },
            new Price { Id = 12, OfferId = 2, RegionId = 2, Value = 4300m },
            new Price { Id = 13, OfferId = 4, RegionId = 2, Value = 620m },
            new Price { Id = 14, OfferId = 7, RegionId = 2, Value = 530m },
            new Price { Id = 15, OfferId = 11, RegionId = 2, Value = 820m },
            new Price { Id = 16, OfferId = 19, RegionId = 2, Value = 8700m },
            new Price { Id = 17, OfferId = 27, RegionId = 2, Value = 82m },

            // Екатеринбург (RegionId = 3) - цены средние
            new Price { Id = 18, OfferId = 1, RegionId = 3, Value = 60m },
            new Price { Id = 19, OfferId = 2, RegionId = 3, Value = 4200m },
            new Price { Id = 20, OfferId = 4, RegionId = 3, Value = 600m },
            new Price { Id = 21, OfferId = 7, RegionId = 3, Value = 520m },
            new Price { Id = 22, OfferId = 11, RegionId = 3, Value = 800m },
            new Price { Id = 23, OfferId = 19, RegionId = 3, Value = 8500m },
            new Price { Id = 24, OfferId = 23, RegionId = 3, Value = 330m },
            new Price { Id = 25, OfferId = 27, RegionId = 3, Value = 80m },

            // Казань (RegionId = 4) - цены чуть выше
            new Price { Id = 26, OfferId = 1, RegionId = 4, Value = 63m },
            new Price { Id = 27, OfferId = 2, RegionId = 4, Value = 4400m },
            new Price { Id = 28, OfferId = 4, RegionId = 4, Value = 630m },
            new Price { Id = 29, OfferId = 7, RegionId = 4, Value = 540m },
            new Price { Id = 30, OfferId = 11, RegionId = 4, Value = 830m },
            new Price { Id = 31, OfferId = 19, RegionId = 4, Value = 8800m },
            new Price { Id = 32, OfferId = 27, RegionId = 4, Value = 83m },

            // Новосибирск (RegionId = 5) - цены средние
            new Price { Id = 33, OfferId = 1, RegionId = 5, Value = 61m },
            new Price { Id = 34, OfferId = 2, RegionId = 5, Value = 4250m },
            new Price { Id = 35, OfferId = 4, RegionId = 5, Value = 610m },
            new Price { Id = 36, OfferId = 7, RegionId = 5, Value = 525m },
            new Price { Id = 37, OfferId = 11, RegionId = 5, Value = 810m },
            new Price { Id = 38, OfferId = 19, RegionId = 5, Value = 8600m },
            new Price { Id = 39, OfferId = 23, RegionId = 5, Value = 340m },
            new Price { Id = 40, OfferId = 27, RegionId = 5, Value = 81m }
        );
    }
}