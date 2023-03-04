using FoodService.Models;

namespace FoodService.Repositories;

public interface IFoodRepository : IGenericRepository<Food>
{
    Task<IEnumerable<Food>> GetByCategory(string catgoryCode);
}
