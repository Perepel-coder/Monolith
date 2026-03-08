namespace Monolith.Application.DTO;

public class OrderResult
{
    public bool Success { get; set; }
    public OrderDto? Order { get; set; }
    public string Message { get; set; }

    public static OrderResult Successful(OrderDto order) => new()
    {
        Success = true,
        Order = order,
        Message = "Заказ успешно создан"
    };
}