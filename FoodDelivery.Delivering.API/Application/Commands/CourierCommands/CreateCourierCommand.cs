using MediatR;
using NetTopologySuite.Geometries;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record CreateCourierCommand(long CourierId, string UserName, string PhoneNumber, string WorkAddress
        ) : IRequest<bool>;
}
