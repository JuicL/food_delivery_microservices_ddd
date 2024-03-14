using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Delivering.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryController : Controller
    {
        private readonly ILogger<DeliveryController> _logger;
        private readonly IMediator _mediator;

        public DeliveryController(
            ILogger<DeliveryController> logger,
            IMediator mediator)
        {
            _logger = logger ?? throw new NullReferenceException(nameof(logger));
            _mediator = mediator ?? throw new NullReferenceException(nameof(mediator)); ;
        }


    }
}
