using Monolith.Application.DTO;
using Monolith.Application.Interfaces;
using Monolith.Applications.Interfaces;

namespace Monolith.Console;

public class MenuService : IMenuService
{
    private readonly IOfferService _offerService;
    private readonly ICategoryService _categoryService;
    private readonly IRegionService _regionService;
    private readonly IOrderProcessorService _orderProcessor;
    private readonly IOrderConfirmationService _confirmationService;

    private RegionDto _currentRegion;
    private List<int> _selectedCategoryIds = new List<int>();

    public MenuService(
        IOfferService offerService,
        ICategoryService categoryService,
        IRegionService regionService,
        IOrderConfirmationService confirmationService,
        IOrderProcessorService orderProcessor)
    {
        _offerService = offerService;
        _categoryService = categoryService;
        _regionService = regionService;
        _orderProcessor = orderProcessor;
        _confirmationService = confirmationService;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Первоначальный выбор региона
            if (_currentRegion == null)
                await SelectInitialRegion();

            // Основной цикл меню
            while (!cancellationToken.IsCancellationRequested)
                await ShowMainMenu();
        }
        catch (Exception ex)
        {
            UIHelper.ShowMessage("Произошла критическая ошибка. Перезапустите приложение.", MessageType.Error);
            throw;
        }
    }

    private async Task SelectInitialRegion()
    {
        var regions = await _regionService.GetAllRegionsAsync();
        _currentRegion = await UIHelper.SelectRegion(regions);

        UIHelper.ShowMessage($"Регион установлен: {_currentRegion.Name}", MessageType.Success);
        await Task.Delay(1000);
    }

    private async Task ShowMainMenu()
    {
        var choice = UIHelper.ShowMainMenu(_currentRegion.Name);

        switch (choice)
        {
            case MainMenuOption.ShowAllOffers:
                await HandleShowAllOffers();
                break;
            case MainMenuOption.SelectCategories:
                await HandleSelectCategories();
                break;
            case MainMenuOption.ChangeRegion:
                await HandleChangeRegion();
                break;
            case MainMenuOption.ShowFileInfo:
                ShowFileInformation();
                break;
            case MainMenuOption.Exit:
                Environment.Exit(0);
                break;
        }
    }

    private void ShowFileInformation()
    {
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MyDB.db");
        var ordersPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Orders");

        UIHelper.ShowFilePaths(dbPath, ordersPath);
        UIHelper.WaitForKey();
    }

    private async Task HandleShowAllOffers()
    {
        var offers = await _offerService.GetOffers(
            _currentRegion.Id,
            new List<int>());

        await DisplayAndProcessOffers(offers);
    }

    private async Task HandleSelectCategories()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        _selectedCategoryIds = await UIHelper.SelectCategories(categories);

        if (_selectedCategoryIds.Any())
        {
            var offers = await _offerService.GetOffers(
                _currentRegion.Id,
                _selectedCategoryIds);

            await DisplayAndProcessOffers(offers);
        }
        else
        {
            UIHelper.ShowMessage("Категории не выбраны. Показываем все товары.", MessageType.Warning);
            await HandleShowAllOffers();
        }
    }

    private async Task HandleChangeRegion()
    {
        var regions = await _regionService.GetAllRegionsAsync();
        _currentRegion = await UIHelper.SelectRegion(regions);
        _selectedCategoryIds.Clear();

        UIHelper.ShowMessage($"Регион изменен на: {_currentRegion.Name}", MessageType.Success);
        await Task.Delay(1000);
    }

    private async Task DisplayAndProcessOffers(List<OfferDto> offers)
    {
        if (!offers.Any())
        {
            UIHelper.ShowMessage("Товары не найдены", MessageType.Error);
            UIHelper.WaitForKey();
            return;
        }

        var selectedOffer = await UIHelper.SelectOffer(offers, _currentRegion.Name);
        await ProcessOfferSelection(selectedOffer);
    }

    private async Task ProcessOfferSelection(OfferDto selectedOffer)
    {
        // Создаем заказ
        var orderResult = await _orderProcessor.ProcessOrderAsync(
            selectedOffer.Id,
            _currentRegion.Id);

        if (!orderResult.Success)
        {
            UIHelper.ShowMessage(orderResult.Message, MessageType.Error);
            UIHelper.WaitForKey();
            return;
        }

        // Показываем детали заказа
        UIHelper.DisplayOrder(orderResult.Order);

        // Спрашиваем подтверждение
        if (UIHelper.ConfirmOrder())
            await ConfirmOrder(orderResult.Order);
        else
            await HandleOrderRejection(selectedOffer, orderResult.Order);
    }

    private async Task HandleOrderRejection(OfferDto selectedOffer, OrderDto order)
    {
        UIHelper.ShowMessage("Анализируем лучшие предложения...", MessageType.Info);

        var specialOffer = await _orderProcessor.CalculateSpecialOfferAsync(
            selectedOffer.Id,
            _currentRegion.Id,
            selectedOffer.Price);

        if (specialOffer.IsEligible &&
            UIHelper.ShowSpecialOffer(specialOffer, selectedOffer.Name))
        {
            var discountedOrder = await _orderProcessor.ProcessOrderAsync(
                selectedOffer.Id,
                _currentRegion.Id);

            if (discountedOrder.Success)
            {
                discountedOrder.Order.Price = specialOffer.DiscountedPrice;
                discountedOrder.Order.OriginalPrice = selectedOffer.Price;
                await ConfirmOrder(discountedOrder.Order);
                return;
            }
        }

        UIHelper.ShowMessage("Заказ отменен", MessageType.Warning);
        UIHelper.WaitForKey();
    }

    private async Task ConfirmOrder(OrderDto order)
    {
        var confirmed = await _confirmationService.ConfirmOrderAsync(order);

        if (confirmed)
            UIHelper.ShowOrderConfirmation(order);
        else
            UIHelper.ShowMessage("Ошибка при оформлении заказа", MessageType.Error);

        UIHelper.WaitForKey();
    }
}
