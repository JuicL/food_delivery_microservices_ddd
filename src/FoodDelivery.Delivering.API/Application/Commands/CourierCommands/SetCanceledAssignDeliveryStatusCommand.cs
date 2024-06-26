﻿using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{

    public record SetCanceledAssignDeliveryStatusCommand(
        long DeliveryId,
        long CourierId
        ) : IRequest<bool>;
}
