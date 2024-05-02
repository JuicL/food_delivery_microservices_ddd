using FoodDelivery.Delivering.API.DTOs;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Queries
{
    public record GetDeliveriesByCourierIdQuery(long CourierId) : IRequest<List<DeliveryResponceDTO>>;
}
