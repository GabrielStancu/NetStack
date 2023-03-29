using DocumentsService.Configurations;
using Microsoft.Extensions.Options;
using Nest;

namespace DocumentsService.Data;

public class ElasticRepositoryFactory : IElasticRepositoryFactory
{
    private readonly ElasticSearchConfiguration _config;

    public ElasticRepositoryFactory(IOptions<ElasticSearchConfiguration> options)
    {
        _config = options.Value;
    }

    public IElasticSearchRepository Create(string indexName)
    {
        var connectionString = _config.ConnectionString;
        var connectionSettings = new ConnectionSettings(new Uri(connectionString!))
            .EnableApiVersioningHeader();
        var client = new ElasticClient(connectionSettings);

        return new ElasticSearchRepository(client, indexName);
    }
}
