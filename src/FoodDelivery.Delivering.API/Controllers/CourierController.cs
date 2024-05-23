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
            var command = new SetAtWorkCourierStatusCommand(courierId,workAddress);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while set at work status. {ex.Message}");
            }
            
        }
        //WorkOff
        [HttpPost]
        [Route("workOff")]
        public async Task<IActionResult> WorkOff(long courierId)
        {
            var command = new SetWorkOffCourierStatusCommand(courierId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while set work off status. {ex.Message}");
            }

        }
        //CancelDelivery
        [HttpPost]
        [Route("cancelDelivery")]
        public async Task<IActionResult> CancelDelivery(long deliveryId)
        {
            var command = new SetCanceledStatusCommand(deliveryId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while canceling delivery. {ex.Message}");
            }
        }

        //AcceptDelivery
        [HttpPost]
        [Route("acceptDelivery")]
        public async Task<IActionResult> AcceptDelivery(long courierId, long deliveryId)
        {
            var command = new AcceptDeliveryCommand(deliveryId,courierId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while accepting delivery. {ex.Message}");
            }
            
        }
        //AtPlaceReceipt
        [HttpPost]
        [Route("atPlaceReceipt")]
        public async Task<IActionResult> AtPlaceReceipt(long deliveryId)
        {
            var command = new SetWaitingReceiveDeliveryStatusCommand(deliveryId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while while set at place delivery status. {ex.Message}");
            }
        }
        //AcceptForDelivery
        [HttpPost]
        [Route("acceptForDelivery")]
        public async Task<IActionResult> AcceptForDelivery(long deliveryId)
        {
            var command = new SetAcceptedForDeliveryStatusCommand(deliveryId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while set accept for delivering status. {ex.Message}");
            }
        }
        //AtDeliveryPlace
        [HttpPost]
        [Route("atDeliveryPlace")]
        public async Task<IActionResult> AtDeliveryPlace(long deliveryId)
        {
            var command = new SetArrivedAtDeliveryLocationStatusCommand(deliveryId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while set at destination place delivery status. {ex.Message}");
            }
        }
        //Delivered
        [HttpPost]
        [Route("delivered")]
        public async Task<IActionResult> Delivered(long deliveryId)
        {
            var command = new SetDeliveredStatusCommand(deliveryId);
            try
            {
                bool result = await _mediator.Send(command);
                return result ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while set delivered status. {ex.Message}");
            }
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
