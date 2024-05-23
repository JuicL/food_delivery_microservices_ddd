using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using FoodDelivery.RestaurantCatalogApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.RestaurantCatalogApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DishController : Controller
    {
        private readonly IDishRepository _dishRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDishTypeRepository _dishTypeRepository;
        private ILogger<DishController> _logger;
        public DishController(IDishRepository dishRepository, IBranchRepository branchRepository, IRestaurantRepository restaurantRepository, IDishTypeRepository dishTypeRepository, ILogger<DishController> logger)
        {
            _dishRepository = dishRepository;
            _branchRepository = branchRepository;
            _restaurantRepository = restaurantRepository;
            _dishTypeRepository = dishTypeRepository;
            _logger = logger;
        }

        #region Create

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DishResponseDTO dishDTO, CancellationToken cancellationToken)
        {
            var dishType = await _dishTypeRepository.GetByName(dishDTO.DishType,cancellationToken);
            _logger.LogInformation("Test log info {1}", "TestArgs");
            if (dishType is null)
                return NotFound($"Dish type: {dishDTO.DishType} not found");
            try
            {
                var dish = new Dish(
                    dishDTO.Name,
                    Weight.CreateFromGram(dishDTO.Weight),
                    new Price(dishDTO.Price),
                    dishType,
                    dishDTO.Ingredients is null ? new() : dishDTO.Ingredients
                    );
                await _dishRepository.CreateAsync(dish, cancellationToken);
                await _dishRepository.UnitOfWork.SaveChangesAsync();

                return Ok(dish.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating Dish.{ex.Message}");
            }
        }
        [HttpPost]
        [Route("branch/{id}")]
        public async Task<IActionResult> CreateForBranchAsync(DishAvaibleDTO dishDTO,int id, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(id, cancellationToken);
            if (branch is null)
                return NotFound("Branch not found");

            var dishType = await _dishTypeRepository.GetByName(dishDTO.DishType, cancellationToken);
            if (dishType is null)
                return NotFound($"Dish type: {dishDTO.DishType} not found");
            try
            {
                var dish = new Dish(
                    dishDTO.Name,
                    Weight.CreateFromGram(dishDTO.Weight),
                    new Price(dishDTO.Price),
                    dishType,
                    dishDTO.Ingredients is null ? new() : dishDTO.Ingredients
                    );
                await _dishRepository.CreateAsync(dish, cancellationToken);
                branch.AddDishes(dish, dishDTO.IsAvaible);
                await _branchRepository.UpdateAsync(branch, cancellationToken);
                await _dishRepository.UnitOfWork.SaveChangesAsync();
                return Ok(dish.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating Dish.{ex.Message}");
            }
            
        }
        [HttpPost]
        [Route("{dishId}/branch/{branchId}")]
        public async Task<IActionResult> CreateForBranchAsync(int dishId, int branchId, bool IsAvaible, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(branchId, cancellationToken);
            if (branch is null)
                return NotFound("Branch not found");
            var dish = await _dishRepository.FindById(dishId, cancellationToken);

            if (dish is null)
                return NotFound("Dish not found");
            try
            {
                branch.AddDishes(dish, IsAvaible);
                await _branchRepository.UpdateAsync(branch, cancellationToken);
                await _dishRepository.UnitOfWork.SaveChangesAsync();
                return Ok("Successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating Dish.{ex.Message}");
            }
        }
        [HttpPost]
        [Route("restaurant/{id}")]
        public async Task<IActionResult> CreateForRestaurantAsync(DishAvaibleDTO dishDTO, int id, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.FindByIdAsync(id, cancellationToken);
            if (restaurant is null)
                return NotFound("Restaurant not found");

            var branches = await _branchRepository.GetBranchesByResaurantIdAsync(id, cancellationToken);
            if (branches is null)
                return NotFound("Branches not found");

            var dishType = await _dishTypeRepository.GetByName(dishDTO.DishType, cancellationToken);
            if (dishType is null)
                return NotFound($"Dish type: {dishDTO.DishType} not found");
            try
            {
                var dish = new Dish(
                    dishDTO.Name,
                    Weight.CreateFromGram(dishDTO.Weight),
                    new Price(dishDTO.Price),
                    dishType,
                    dishDTO.Ingredients is null ? new() : dishDTO.Ingredients
                    );
                await _dishRepository.CreateAsync(dish, cancellationToken);
                foreach (var branch in branches)
                {
                    branch.AddDishes(dish, dishDTO.IsAvaible);
                    await _branchRepository.UpdateAsync(branch, cancellationToken);
                }

                await _dishRepository.UnitOfWork.SaveChangesAsync();
                return Ok(dish.Id);

            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating Dish.{ex.Message}");
            }
        }

        [HttpPost]
        [Route("{dishId}/restaurant/{restaurantId}")]
        public async Task<IActionResult> CreateForRestaurantAsync(int dishId, int restaurantId, bool IsAvaible, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.FindByIdAsync(restaurantId, cancellationToken);
            if (restaurant is null)
                return NotFound("Restaurant not found");
            var dish = await _dishRepository.FindById(dishId, cancellationToken);
            if (dish is null)
                return NotFound("Dish not found");
            var branches = await _branchRepository.GetBranchesByResaurantIdAsync(restaurantId, cancellationToken);
            if (branches is null)
                return NotFound("Branches not found");
            try
            {
                foreach (var branch in branches)
                {
                    branch.AddDishes(dish, IsAvaible);
                    await _branchRepository.UpdateAsync(branch, cancellationToken);
                }
                await _dishRepository.UnitOfWork.SaveChangesAsync();
                return Ok("Successful");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating Dish.{ex.Message}");
            }
        }
        #endregion

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(DishResponseDTO dish, CancellationToken cancellationToken)
        {
            var updateDish = await _dishRepository.FindById(dish.Id, cancellationToken);
            if (updateDish is null)
                return NotFound("Dish not found");

            var dishType = await _dishTypeRepository.GetByName(dish.DishType, cancellationToken);
            if (dishType is null)
                return NotFound($"Dish type: {dish.DishType} not found");
            try
            {
                updateDish.ChangeDishType(dishType);
                updateDish.ChangeIngredients(dish.Ingredients);
                updateDish.ChangeName(dish.Name);
                updateDish.ChangePrice(new Price(dish.Price));
                updateDish.ChangeWeight(Weight.CreateFromGram(dish.Weight));
                await _dishRepository.UpdateAsync(updateDish, cancellationToken);
                await _dishRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating Dish.{ex.Message}");
            }
            return Ok();
        }
        [HttpPut]
        [Route("branch/{branchId}")]

        public async Task<IActionResult> UpdateDishAvaibleAsync(DishResponseDTO dish, int branchId, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(dish.Id, cancellationToken);
            if (branch is null)
                return NotFound("branch not found");
            var updateDish = branch.Dishes.Where(x => x.BranchId == branchId).Select(x => x.Dish).FirstOrDefault();
            if (updateDish is null)
                return NotFound("Dish not found");

            var dishType = await _dishTypeRepository.GetByName(dish.DishType, cancellationToken);
            if (dishType is null)
                return NotFound($"Dish type: {dish.DishType} not found");
            try
            {
                updateDish.ChangeDishType(dishType);
                updateDish.ChangeIngredients(dish.Ingredients);
                updateDish.ChangeName(dish.Name);
                updateDish.ChangePrice(new Price(dish.Price));
                updateDish.ChangeWeight(Weight.CreateFromGram(dish.Weight));
                await _dishRepository.UpdateAsync(updateDish, cancellationToken);
                await _dishRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating Dish.{ex.Message}");

            }
            return Ok();
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var dish = await _dishRepository.FindById(id, cancellationToken);
            if (dish is null) return NotFound("Dish not found");
            return Ok(new DishResponseDTO()
            {
                Id = (int)dish.Id,
                Name = dish.Name,
                Price = dish.Price.Amount,
                Weight = dish.Weight.Gram,
                DishType = dish.DishType.Name,
                Ingredients = dish.Ingredients
            });
        }
        [HttpGet]
        [Route("branch/{id}")]
        public async Task<IActionResult> GetByBranchId(int id, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.FindByIdAsync(id, cancellationToken);
            if (branch is null) return NotFound("Dish not found");

            return Ok(branch.Dishes.Select(x => new DishAvaibleDTO()
            {
                Id = (int)x.Dish.Id,
                Name = x.Dish.Name,
                Price = x.Dish.Price.Amount,
                IsAvaible = x.IsAvaible,
                Weight = x.Dish.Weight.Gram,
                DishType = x.Dish.DishType.Name,
                Ingredients = x.Dish.Ingredients
            }));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var dishesh = await _dishRepository.GetAllDichesAsync(cancellationToken);
            if (dishesh is null) return NotFound("Dishes not found");
            return Ok(dishesh.Select(dish => new DishResponseDTO()
            {
                Id = (int)dish.Id,
                Name = dish.Name,
                Price = dish.Price.Amount,
                Weight = dish.Weight.Gram,
                DishType = dish.DishType.Name,
                Ingredients = dish.Ingredients
            }));
        }
    }
}

    

