namespace DocumentsService.Data;

public interface IElasticRepositoryFactory
{
    IElasticSearchRepository Create(string indexName);
}
