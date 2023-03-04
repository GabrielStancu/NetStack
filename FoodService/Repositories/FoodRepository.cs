using FoodService.Data;
using FoodService.Models;
using MongoDB.Driver;

namespace FoodService.Repositories;

public class FoodRepository : GenericRepository<Food>, IFoodRepository
{
    public FoodRepository(IDatabaseSettings settings) : base(settings.ConnectionString, settings.DatabaseName, settings.FoodCollectionName)
    {
    }

    public async Task<IEnumerable<Food>> GetByCategory(string catgoryCode)
    {
        return await _collection.Find(f => f.CategoryCode == catgoryCode).ToListAsync();
    }
}
