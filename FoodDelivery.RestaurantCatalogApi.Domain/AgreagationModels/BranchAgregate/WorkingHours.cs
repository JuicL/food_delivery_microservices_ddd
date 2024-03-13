using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate
{
    public class WorkingHours : ValueObject
    {
        private WorkingHours() { }

        public WorkingHours(TimeOnly start, TimeOnly end)
        {
            if (end < start) 
                throw new Exception($"End time period({end}) less that start time{start}");

            Start = start;
            End = end;
        }

        public TimeOnly Start { get; }
        public TimeOnly End { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start; 
            yield return End;
        }
    }
}