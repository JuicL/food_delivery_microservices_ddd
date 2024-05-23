using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.RestaurantCatalogApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BranchController : Controller
    {
        public readonly IBranchRepository branchRepository;
        public readonly IRestaurantRepository restaurantRepository;

        public BranchController(IBranchRepository branchRepository, IRestaurantRepository restaurantRepository)
        {
            this.branchRepository = branchRepository;
            this.restaurantRepository = restaurantRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BranchRequestDTO branch, CancellationToken cancellationToken)
        {
            var restaurant = await restaurantRepository.FindByIdAsync(branch.RestaurantId, cancellationToken);
            if (restaurant is null)
            {
                throw new Exception("Restaurant not found");
            }
            try
            {
                var createdBranch = await branchRepository.CreateAsync(
                    new Branch(restaurant,
                        Address.Parse(branch.Address),
                        new WorkingHours(branch.OpenningTime, branch.ClosingTime),
                        branch.IsAvaible, new List<DishAvaible>()),
                    cancellationToken);
                await branchRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                return Ok(createdBranch.Id);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while creating branch. {ex.Message}");
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var branch = await branchRepository.FindByIdAsync(id, cancellationToken);
            if (branch is null)
            {
                return NotFound("Branch not found");
            }
            return Ok(new BranchResponseDTO()
            {
                Id = (int)branch.Id,
                Address = branch.Address.GetFullAddress(),
                OpenningTime = branch.WorkingHours.Start,
                ClosingTime = branch.WorkingHours.End,
                IsAvaible = branch.IsAvailable,
                Dishes = branch.Dishes.Select(x=> new DishAvaibleResponseDTO() { Id = x.Dish.Id, Name = x.Dish.Name,
                DishType = x.Dish.DishType.Name, Ingredients = x.Dish.Ingredients, IsAvaible = x.IsAvaible, Price = x.Dish.Price.Amount,
                Weight = x.Dish.Weight.Gram}).ToList(),
                RestaurantId = (int)branch.Restaurant.Id,
            });
        }
        [HttpGet]
        [Route("restaurant/{id}")]
        public async Task<IActionResult> GetByResaurantIdAsync(int id, CancellationToken cancellationToken)
        {
            var branch = await branchRepository.GetBranchesByResaurantIdAsync(id, cancellationToken);
            if (branch is null)
            {
                return NotFound("Branch not found");
            }
            return Ok(branch.Select( x=> new BranchResponseDTO()
            {
                Id = (int)x.Id,
                Address = x.Address.GetFullAddress(),
                OpenningTime = x.WorkingHours.Start,
                ClosingTime = x.WorkingHours.End,
                IsAvaible = x.IsAvailable,
                Dishes =null,
                RestaurantId = (int)x.Restaurant.Id,
            }));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var branch = await branchRepository.GetAllBranchesAsync(cancellationToken);
          
            return Ok(branch.Select(x => new BranchResponseDTO()
            {
                Id = (int)x.Id,
                Address = x.Address.GetFullAddress(),
                OpenningTime = x.WorkingHours.Start,
                ClosingTime = x.WorkingHours.End,
                IsAvaible = x.IsAvailable,
                Dishes = null,
                RestaurantId = (int)x.Restaurant.Id,
            }));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(BranchRequestDTO branch,CancellationToken cancellationToken) 
        {
            var updatedBranch = await branchRepository.FindByIdAsync(branch.Id, cancellationToken);
            if(updatedBranch is null)
                return NotFound("Branch not found");
            try
            {
                updatedBranch.ChangeAddress(Address.Parse(branch.Address));
                updatedBranch.ChangeAvaibleStatus(branch.IsAvaible);
                updatedBranch.ChangeWorkingHours(new WorkingHours(branch.OpenningTime, branch.ClosingTime));
                await branchRepository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while updating branch. {ex.Message}");
            }
            
            return Ok();
        }
    }
}

    

