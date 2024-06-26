﻿using FoodDelivery.Delivering.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Delivering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
        // GetById
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var getDeliveryQuery = new GetDeliveryByIdQuery(id);
            try
            {
                var delivery = await _mediator.Send(getDeliveryQuery);
                if (delivery is null)
                    return NotFound("Delivery not found");
                return Ok(delivery);
            }
            catch (Exception ex)
            {
                return BadRequest($"Something gonna wrong {ex.Message}");
            }
        }
        // GetBy courier
        [HttpGet]
        [Route("/courier/{id}")]
        public async Task<IActionResult> GetByCourierId(long id)
        {
            var getDeliveryQuery = new GetDeliveriesByCourierIdQuery(id);
            try
            {
                var delivery = await _mediator.Send(getDeliveryQuery);
                return Ok(delivery);
            }
            catch (Exception ex)
            {
                return BadRequest($"Something gonna wrong {ex.Message}");
            }

        }
        // GetBy courier assigned deliveries

        // GetBy user
        [HttpGet]
        [Route("/recipient/{id}")]
        public async Task<IActionResult> GetByRecipientId(long id)
        {
            var getDeliveryQuery = new GetDeliveriesByRecipientIdQuery(id);
            var delivery = await _mediator.Send(getDeliveryQuery);

            return Ok(delivery);
        }
    }
}
