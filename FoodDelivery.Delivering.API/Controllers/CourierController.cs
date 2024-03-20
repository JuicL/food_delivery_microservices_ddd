using FoodDelivery.Delivering.API.Application.Commands.CourierCommands;
using FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
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
        [HttpPost]
        [Route("atWork")]
        public async Task<IActionResult> AtWork(long courierId, string workAddress)
        {
            var command = new SetAtWorkCourierStatusCommand(courierId,WorkAddress.Parse(workAddress));
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
        //WorkOff
        [HttpPost]
        [Route("workOff")]
        public async Task<IActionResult> WorkOff(long courierId)
        {
            var command = new SetWorkOffCourierStatusCommand(courierId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
        //CancelDelivery
        [HttpPost]
        [Route("cancelDelivery")]
        public async Task<IActionResult> CancelDelivery(long deliveryId)
        {
            var command = new SetCanceledStatusCommand(deliveryId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }

        //AcceptDelivery
        [HttpPost]
        [Route("acceptDelivery")]
        public async Task<IActionResult> AcceptDelivery(long courierId, long deliveryId)
        {
            var command = new AcceptDeliveryCommand(deliveryId,courierId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
        //AtPlaceReceipt
        [HttpPost]
        [Route("atPlaceReceipt")]
        public async Task<IActionResult> AtPlaceReceipt(long deliveryId)
        {
            var command = new SetWaitingReceiveDeliveryStatusCommand(deliveryId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
        //AcceptForDelivery
        [HttpPost]
        [Route("acceptForDelivery")]
        public async Task<IActionResult> AcceptForDelivery(long deliveryId)
        {
            var command = new SetAcceptedForDeliveryStatusCommand(deliveryId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
        //AtDeliveryPlace
        [HttpPost]
        [Route("atDeliveryPlace")]
        public async Task<IActionResult> AtDeliveryPlace(long deliveryId)
        {
            var command = new SetArrivedAtDeliveryLocationStatusCommand(deliveryId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }
        //Delivered
        [HttpPost]
        [Route("delivered")]
        public async Task<IActionResult> Delivered(long deliveryId)
        {
            var command = new SetDeliveredStatusCommand(deliveryId);
            bool result = await _mediator.Send(command);
            return result ? Ok() : BadRequest();
        }

        //GetAllAssignedDeliveries
        [HttpGet]
        [Route("deliveries/{courierId}")]
        public async Task<IActionResult> GetAllAssignedDeliveries(long courierId)
        {
            return Ok();
            //var command = new SetDeliveredStatusCommand(deliveryId);
            //bool result = await _mediator.Send(command);
            //return result ? Ok() : BadRequest();
        }
    }
}
