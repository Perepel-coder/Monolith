using Microsoft.Extensions.Logging;
using Monolith.Application.DTO;
using Monolith.Application.Interfaces;
using System.Text.Json;

namespace Monolith.Infrastructure.Services;

public class OrderFileStorage : IOrderFileStorage
{
    private readonly string _ordersDirectory;
    private readonly JsonSerializerOptions _jsonOptions;

    public OrderFileStorage()
    {
        _ordersDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Orders");
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        // Создаем директорию, если её нет
        if (!Directory.Exists(_ordersDirectory))
        {
            Directory.CreateDirectory(_ordersDirectory);
        }
    }

    public async Task SaveOrderAsync(OrderDto order)
    {
        var fileName = $"Order_{order.CreatedAt:yyyyMMdd_HHmmss}_{order.OfferId}.json";
        var filePath = Path.Combine(_ordersDirectory, fileName);

        var orderDocument = new
        {
            Order = order,
            Metadata = new
            {
                SavedAt = DateTime.Now,
                FileName = fileName,
                SystemInfo = Environment.MachineName
            }
        };

        var json = JsonSerializer.Serialize(orderDocument, _jsonOptions);
        await File.WriteAllTextAsync(filePath, json);
    }

    public async Task<List<OrderDto>> GetSavedOrdersAsync()
    {
        var files = Directory.GetFiles(_ordersDirectory, "Order_*.json");
        var orders = new List<OrderDto>();

        foreach (var file in files)
        {
            try
            {
                var json = await File.ReadAllTextAsync(file);
                var document = JsonSerializer.Deserialize<OrderDocument>(json);
                if (document?.Order != null)
                {
                    orders.Add(document.Order);
                }
            }
            catch
            {
            }
        }

        return orders.OrderByDescending(o => o.CreatedAt).ToList();
    }

    public async Task<OrderDto?> GetOrderAsync(string orderFileName)
    {
        var filePath = Path.Combine(_ordersDirectory, orderFileName);
        if (!File.Exists(filePath))
            return null;

        var json = await File.ReadAllTextAsync(filePath);
        var document = JsonSerializer.Deserialize<OrderDocument>(json);

        return document?.Order;
    }

    private class OrderDocument
    {
        public OrderDto Order { get; set; }
        public object Metadata { get; set; }
    }
}