using FoodService.Models;

namespace FoodService.Repositories;

public interface IGenericRepository<T> where T : ModelBase
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(string id);
    Task Save(T category);
    Task Delete(string id);
    Task SoftDelete(string id);
}
