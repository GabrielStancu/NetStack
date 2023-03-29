using Nest;

namespace DocumentsService.Data;

public interface IElasticSearchRepository
{
    Task<bool> AddOrUpdate<T>(T document) where T : class;
    Task<bool> AddOrUpdateBulk<T>(IEnumerable<T> documents) where T : class;
    Task CreateIndexIfNotExists(string indexName);
    Task<T> Get<T>(string key) where T : class;
    Task<List<T>?> GetAll<T>() where T : class;
    ElasticSearchRepository Index(string indexName);
    Task<List<T>?> Query<T>(QueryContainer predicate) where T : class;
    Task<bool> Remove<T>(string key) where T : class;
    Task<bool> RemoveAll<T>() where T : class;
}
