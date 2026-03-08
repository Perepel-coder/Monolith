using Monolith.Application.DTO;
using Spectre.Console;

namespace Monolith.Console;

public enum MainMenuOption
{
    ShowAllOffers,
    SelectCategories,
    ChangeRegion,
    ShowFileInfo,
    Exit
}

public enum MessageType
{
    Success,
    Error,
    Warning,
    Info
}

public static class UIHelper
{
    /// <summary>
    /// Показывает заголовок приложения
    /// </summary>
    public static void ShowHeader()
    {
        AnsiConsole.Write(
            new FigletText("МАГАЗИН НА ДИВАНЕ")
                .LeftJustified()
                .Color(Color.Cyan1));

        AnsiConsole.Write(
            new Rule("[yellow]Система управления товарами[/]")
                .RuleStyle("grey")
                .Centered());
    }

    /// <summary>
    /// Показывает меню выбора региона
    /// </summary>
    public static async Task<RegionDto> SelectRegion(List<RegionDto> regions)
    {
        AnsiConsole.Clear();
        ShowHeader();

        AnsiConsole.MarkupLine("[yellow]Пожалуйста, установите регион:[/]");

        var selectedRegionName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Доступные регионы:")
                .PageSize(10)
                .AddChoices(regions.Select(r => r.Name)));

        return regions.First(r => r.Name == selectedRegionName);
    }

    /// <summary>
    /// Показывает главное меню и возвращает выбранный пункт
    /// </summary>
    public static MainMenuOption ShowMainMenu(string currentRegion)
    {
        AnsiConsole.Clear();
        ShowHeader();

        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[yellow]ГЛАВНОЕ МЕНЮ (Регион: {currentRegion})[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "1. Показать все товары",
                    "2. Выбрать категорию",
                    "3. Поменять регион",
                    "4. Где хранятся файлы?",
                    "5. Выход"
                }));

        return choice switch
        {
            "1. Показать все товары" => MainMenuOption.ShowAllOffers,
            "2. Выбрать категорию" => MainMenuOption.SelectCategories,
            "3. Поменять регион" => MainMenuOption.ChangeRegion,
            "4. Где хранятся файлы?" => MainMenuOption.ShowFileInfo,
            _ => MainMenuOption.Exit
        };
    }

    /// <summary>
    /// Показывает мультивыбор категорий
    /// </summary>
    public static async Task<List<int>> SelectCategories(List<CategoryDto> categories)
    {
        AnsiConsole.Clear();
        ShowHeader();

        var selectedCategories = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                .Title("[yellow]Выберите категории (пробел - выбор, Enter - подтвердить):[/]")
                .NotRequired()
                .PageSize(15)
                .InstructionsText("[grey](Нажмите пробел для выбора, Enter для подтверждения)[/]")
                .AddChoices(categories.Select(c => c.Name)));

        return categories
            .Where(c => selectedCategories.Contains(c.Name))
            .Select(c => c.Id)
            .ToList();
    }

    /// <summary>
    /// Показывает таблицу товаров
    /// </summary>
    public static async Task<OfferDto> SelectOffer(List<OfferDto> offers, string regionName)
    {
        AnsiConsole.Clear();
        ShowHeader();

        var table = new Table()
            .Border(TableBorder.Rounded)
            .Title($"[yellow]Товары в регионе {regionName}[/]")
            .AddColumn("[u]№[/]")
            .AddColumn("[u]Товар[/]")
            .AddColumn("[u]Категории[/]")
            .AddColumn("[u]Цена[/]");

        for (int i = 0; i < offers.Count; i++)
        {
            var offer = offers[i];
            table.AddRow(
                (i + 1).ToString(),
                offer.Name,
                string.Join(", ", offer.Categories.Take(3)) + (offer.Categories.Count > 3 ? "..." : ""),
                $"[green]{offer.Price:N0} руб.[/]"
            );
        }

        AnsiConsole.Write(table);

        var selectedOfferName = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Выберите товар:[/]")
                .PageSize(15)
                .AddChoices(offers.Select(o => o.Name)));

        return offers.First(o => o.Name == selectedOfferName);
    }

    /// <summary>
    /// Отображает детали заказа
    /// </summary>
    public static void DisplayOrder(OrderDto order)
    {
        var orderPanel = new Panel(
            Align.Center(new Markup(
                $"[bold]ЗАКАЗ[/]\n\n" +
                $"[yellow]Товар:[/] {order.OfferName}\n" +
                $"[yellow]Регион:[/] {order.RegionName}\n" +
                $"[yellow]Цена:[/] [bold green]{order.Price:N0} руб.[/]\n" +
                (order.OriginalPrice.HasValue ?
                    $"[yellow]Скидка:[/] {order.OriginalPrice.Value - order.Price:N0} руб.\n" : "") +
                $"[yellow]Дата:[/] {order.CreatedAt:dd.MM.yyyy HH:mm}"
            ))
        )
        {
            Border = BoxBorder.Double,
            Header = new PanelHeader("[cyan]ДЕТАЛИ ЗАКАЗА[/]"),
            Padding = new Padding(2, 1, 2, 1)
        };

        AnsiConsole.Write(orderPanel);
    }

    /// <summary>
    /// Запрашивает подтверждение заказа
    /// </summary>
    public static bool ConfirmOrder() => AnsiConsole.Confirm("Оформляем заявку?");

    /// <summary>
    /// Показывает специальное предложение
    /// </summary>
    public static bool ShowSpecialOffer(SpecialOfferResult offer, string offerName)
    {
        var panel = new Panel(
            Align.Center(new Markup(
                $"[green]СПЕЦИАЛЬНОЕ ПРЕДЛОЖЕНИЕ![/]\n\n" +
                $"[yellow]Вы выбрали самый дешевый товар в категории![/]\n" +
                $"Товар: [bold]{offerName}[/]\n" +
                $"[green]Предлагаем вам скидку {offer.DiscountPercent}%![/]\n" +
                $"Сумма скидки: [bold]{offer.DiscountAmount:N0} руб.[/]\n" +
                $"[bold]Цена со скидкой: [green]{offer.DiscountedPrice:N0} руб.[/][/]"
            ))
        )
        {
            Border = BoxBorder.Rounded,
            Header = new PanelHeader("[green]СПЕЦПРЕДЛОЖЕНИЕ[/]")
        };

        AnsiConsole.Write(panel);
        return AnsiConsole.Confirm("Принять предложение?");
    }

    /// <summary>
    /// Показывает подтверждение заказа
    /// </summary>
    public static void ShowOrderConfirmation(OrderDto order)
    {
        var orderNumber = new Random().Next(10000, 99999);

        var panel = new Panel(
            Align.Center(new Markup(
                $"[green]✓ ЗАКАЗ УСПЕШНО ОФОРМЛЕН![/]\n\n" +
                $"[yellow]Номер заказа:[/] [bold]#{orderNumber}[/]\n" +
                $"[yellow]Товар:[/] {order.OfferName}\n" +
                $"[yellow]Сумма:[/] [bold]{order.Price:N0} руб.[/]\n"
            ))
        )
        {
            Border = BoxBorder.Rounded,
            Header = new PanelHeader("[green]ПОДТВЕРЖДЕНИЕ[/]")
        };

        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Показывает информационное сообщение
    /// </summary>
    public static void ShowMessage(string message, MessageType type)
    {
        var color = type switch
        {
            MessageType.Success => "green",
            MessageType.Error => "red",
            MessageType.Warning => "yellow",
            MessageType.Info => "blue",
            _ => "white"
        };

        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }

    /// <summary>
    /// Ожидает нажатия клавиши
    /// </summary>
    public static void WaitForKey()
    {
        AnsiConsole.Markup("\n[grey]Нажмите любую клавишу для продолжения...[/]");
        System.Console.ReadKey(true);
    }

    public static void ShowFilePaths(string dbPath, string ordersPath)
    {
        var panel = new Panel(
        Align.Left(new Markup(
            $"[bold]ИНФОРМАЦИЯ О ФАЙЛАХ[/]\n\n" +
            $"[yellow]База данных:[/]\n" +
            $"{dbPath}\n\n" +
            $"[yellow]Папка с заказами:[/]\n" +
            $"{ordersPath}"
        ))
        )
        {
            Border = BoxBorder.Rounded,
            Header = new PanelHeader("[cyan]ФАЙЛЫ ПРИЛОЖЕНИЯ[/]"),
            Padding = new Padding(2, 1, 2, 1)
        };

        AnsiConsole.Write(panel);
    }
}