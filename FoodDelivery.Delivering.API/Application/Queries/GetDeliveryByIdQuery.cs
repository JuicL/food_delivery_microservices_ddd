using FoodDelivery.Delivering.API.DTOs;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Queries
{
    public record GetDeliveryByIdQuery(long DeliveryId) : IRequest<DeliveryResponceDTO?>;
}
