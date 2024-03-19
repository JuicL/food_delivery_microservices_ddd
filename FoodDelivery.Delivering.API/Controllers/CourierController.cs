using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Delivering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CourierController : ControllerBase
    {
        private readonly ILogger<CourierController> _logger;
        private readonly IMediator _mediator;

        public CourierController(
            ILogger<CourierController> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
            _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        }
        //AtWork
        //WorkOff
        //CancelDelivery
        
        //AcceptDelivery
        //AtPlaceReceipt
        //AcceptForDelivery
        //AtDeliveryPlace
        //Delivered

        //GetAllAssignedDeliveries

    }
}
