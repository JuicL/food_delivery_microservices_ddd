using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.RestaurantCatalogApi.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IRestaurantRepository restaurantRepository;

        public RestaurantController(IRestaurantRepository restaurantRepository)
        {
            this.restaurantRepository = restaurantRepository;
        }
        /// <summary>
        /// HTTP_POST: api/v1/restaurant
        /// </summary>
        /// <param name="restaurantDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RestaurantRequestDTO restaurantDTO, CancellationToken cancellationToken)
        {
            await restaurantRepository.CreateAsync(
                new Restaurant(
                    restaurantDTO.Name,
                    new List<Branch>()), 
                cancellationToken);
            await restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok(restaurantDTO);
        }

        /// <summary>
        /// HTTP_GET: api/v1/restaurant/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository.FindByIdAsync(id, cancellationToken);
            if (restaurant is null)
            {
                return NotFound($"Restaurant with id {id} not found");
            }

            return Ok(new  RestaurantResponseDTO()
            {
                Id = (int)restaurant.Id,
                Name = restaurant.Name,
                Branches = restaurant.Branches.Select(x => new BranchResponseDTO()
                {
                    Id = (int)x.Id,
                    Address = x.Address.GetFullAddress(),
                    OpenningTime = x.WorkingHours.Start,
                    ClosingTime = x.WorkingHours.End,
                    RestaurantId = (int)x.Restaurant.Id,
                    IsAvaible = x.IsAvailable
                }).ToList()
            });
        }
        /// <summary>
        /// HTTP_GET: api/v1/restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int id, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository.GetAllAsync(cancellationToken);
            if (restaurant is null)
            {
                return NotFound($"Restaurant with id {id} not found");
            }

            return Ok(restaurant.Select(x => new RestaurantResponseDTO()
            {
                Id = (int)x.Id,
                Name = x.Name,
                Branches = x.Branches.Select(x => new BranchResponseDTO()
                {
                    Id = (int)x.Id,
                    Address = x.Address.GetFullAddress(),
                    OpenningTime = x.WorkingHours.Start,
                    ClosingTime = x.WorkingHours.End,
                    RestaurantId = (int)x.Restaurant.Id,
                    IsAvaible = x.IsAvailable
                }).ToList()
            }).ToList()
            );
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RestaurantRequestDTO restaurant, CancellationToken cancellationToken)
        {
            var updatedRestaurant = await restaurantRepository.FindByIdAsync(restaurant.Id, cancellationToken);
            if (updatedRestaurant is null)
            {
                return NotFound($"Restaurant with id {restaurant.Id} not found");
            }
            updatedRestaurant.ChangeName(restaurant.Name);
            await restaurantRepository.UpdateAsync(updatedRestaurant, cancellationToken);
            await restaurantRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok(restaurant);
        }
    }
}
