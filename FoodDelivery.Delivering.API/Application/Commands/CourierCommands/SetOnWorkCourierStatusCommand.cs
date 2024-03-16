﻿using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetOnWorkCourierStatusCommand(long CourierId, WorkAddress WorkAddress) : IRequest<bool>;
}