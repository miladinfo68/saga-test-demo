using Shared.Entities;
using Shared.Enums;

namespace Shared.Dtos;

public record OrderItemDtoInStockDb(string CustomerId, long ProductId, int Quantity, decimal Price, int? Supply = null);
public record OrderItemDto(long ProductId, int Quantity, decimal Price);
public record ProductDto(int Id, string Name, int Quantity, decimal Price);

public record AddressDto(string Line, string Province, string District)
{
    public static implicit operator Address(AddressDto dto) =>
         new AddressDto(dto.Line, dto.Province, dto.District);
}
public record StockDto(long ProductId);
public record OrderDto(string CustomerId, IEnumerable<OrderItemDto> OrderItems, AddressDto? AddressDto);
public record CreatedOrderDto(double OrderId,OrderStatus Status ,decimal Total ,DateTime OrderDate , OrderDto OrderDto);


public record PaymentDto(
    double OrderId,
    string CustomerId,
    string OrderItemIds,
    string CardName,
    string CardNumber,
    string Expiration,
    string Cvv,
    decimal TotalPrice,
    DateTime PaymentDate
    );

