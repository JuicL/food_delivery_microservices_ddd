using FoodDelivery.OrderApi.DTOs;
using MediatR;
namespace FoodDelivery.OrderApi.Application.Queries
{
    public record GetOrderRequestByIdQuery(long OrderId): IRequest<OrderResponseDTO>;
}
