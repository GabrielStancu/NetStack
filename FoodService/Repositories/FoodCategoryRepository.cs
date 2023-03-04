using FoodService.Data;
using FoodService.Models;

namespace FoodService.Repositories;

public class FoodCategoryRepository : GenericRepository<FoodCategory>, IFoodCategoryRepository
{
    public FoodCategoryRepository(IDatabaseSettings settings) : base(settings.ConnectionString, settings.DatabaseName, settings.FoodCollectionName)
    {
    }
}
