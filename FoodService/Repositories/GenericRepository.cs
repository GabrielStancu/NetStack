using FoodService.Models;
using MongoDB.Driver;

namespace FoodService.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
{
    protected readonly IMongoCollection<T> _collection;

    public GenericRepository(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task Delete(string id)
    {
        await _collection.DeleteOneAsync(fc => fc.Id == id);
    }

    public async Task<T> Get(string id)
    {
        return await _collection.Find(fc => fc.Id.Equals(id))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _collection.Find(FilterDefinition<T>.Empty)
            .ToListAsync();
    }

    public async Task Save(T category)
    {
        var data = await Get(category.Id);

        if (data is null)
        {
            await _collection.InsertOneAsync(category);
        }
        else
        {
            await _collection.ReplaceOneAsync(
                fc => fc.Id.Equals(category.Id), category
            );
        }
    }

    public async Task SoftDelete(string id)
    {
        var data = await Get(id);
        if (data is null)
            return;

        data.Deleted = true;
        await _collection.ReplaceOneAsync(
            fc => fc.Id.Equals(id), data
        );
    }
}
