﻿using DDD.Domain.Contracts;
using NetTopologySuite.Geometries;


namespace FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate
{
    public interface ICourierRepository : IRepository<Courier>
    {
        Task<Courier> CreateAsync(Courier courier);
        Task<Courier> UpdateAsync (Courier courier);
        Task<Courier> GetByIdAsync (long id);
        Task<List<Courier>> GetAllAsync();
        Task<List<Courier>> GellAllFreeAsync();
        Task<List<Courier>> GellAllFreeNearestAsync(Point point, double? diameters = null);
    }
}
