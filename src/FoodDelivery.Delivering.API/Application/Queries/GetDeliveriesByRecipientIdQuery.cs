using FoodDelivery.Delivering.API.DTOs;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Queries
{
    public record GetDeliveriesByRecipientIdQuery(long RecipientId) : IRequest<List<DeliveryResponceDTO>>;
}
