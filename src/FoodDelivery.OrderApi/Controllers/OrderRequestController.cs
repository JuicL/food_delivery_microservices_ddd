using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.Queries;
using FoodDelivery.OrderApi.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.OrderApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderRequestController : ControllerBase
    {
        private readonly ILogger<OrderRequestController> _logger;
        private readonly IMediator _mediator;

        public OrderRequestController(
            ILogger<OrderRequestController> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
            _mediator = mediator ?? throw new NullReferenceException(nameof(mediator));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderRequest"></param>
        /// <returns>Id of order request</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateOrderRequestDTO orderRequest)
        {
            var createCommand = new CreateOrderRequestCommand(
                orderRequest.UserId,
                orderRequest.UserName,
                orderRequest.Phone, 
                orderRequest.DeliveryAddress,
                orderRequest.RestaurantName,
                orderRequest.BranchId,
                orderRequest.RestaurantAddress, 
                orderRequest.PaymentMethod,
                orderRequest.OrderTime,
                orderRequest.Dishes,
                orderRequest.Description);

            var result = await _mediator.Send(createCommand);
            if (result == 0)
                return BadRequest("Something gonna wrong");
            
            return Ok(new { Id = result });
        }

        [HttpPost]
        [Route("cancel/{orderId}")]
        public async Task<IActionResult> CancelAsync([FromQuery] long orderId)
        {
            var cancelCommand = new SetCanceledOrderStatusCommand(orderId);
            var result = await _mediator.Send(cancelCommand);
            return Ok(result);
        } 
        
        [HttpGet]
        [Route("{orderId}")]
        public async Task<IActionResult> GetOrederById([FromQuery] long orderId)
        {
            var getQuery = new GetOrderRequestByIdQuery(orderId);
            var result = await _mediator.Send(getQuery);
            return Ok(result);
        }
    }
}
