using FoodDelivery.BasketApi.Extensions;
using FoodDelivery.BasketApi.Model;
using FoodDelivery.BasketApi.Repositories;
using Grpc.Core;

namespace FoodDelivery.BasketApi.Grpc
{
    public class BasketService(IBasketRepository repository) : Basket.BasketBase
    {
        public override async Task<CustomerBasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            var userId = context.GetUserIdentity();
            if (string.IsNullOrEmpty(userId))
            {
                return new();
            }

            var data = await repository.GetBasketAsync(userId);
            if (data is not null)
            {
                return MapToCustomerBasketResponse(data);
            }

            return new();
        }

        public override async Task<CustomerBasketResponse> UpdateBasket(UpdateBasketRequest request, ServerCallContext context)
        {
            var userId = context.GetUserIdentity();
            if (string.IsNullOrEmpty(userId))
            {
                return new();
            }

            var customerBasket = MapToCustomerBasket(userId, request);
            var response = await repository.UpdateBasketAsync(customerBasket);
            if (response is null)
            {
                throw new Exception("Failed update basket");
            }

            return MapToCustomerBasketResponse(response);
        }

        public override async Task<DeleteBasketResponse> DeleteBasket(DeleteBasketRequest request, ServerCallContext context)
        {
            var userId = context.GetUserIdentity();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            await repository.DeleteBasketAsync(userId);
            return new();
        }

        private static CustomerBasketResponse MapToCustomerBasketResponse(CustomerBasket customerBasket)
        {
            var response = new CustomerBasketResponse();

            foreach (var item in customerBasket.Items)
            {
                response.Items.Add(new BasketItem()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Units,
                });
            }

            return response;
        }

        private static CustomerBasket MapToCustomerBasket(string userId, UpdateBasketRequest customerBasketRequest)
        {
            var response = new CustomerBasket
            {
                BuyerId = userId
            };

            foreach (var item in customerBasketRequest.Items)
            {
                response.Items.Add(new()
                {
                    ProductId = item.ProductId,
                    Units = item.Quantity,
                });
            }

            return response;
        }
    }
}
