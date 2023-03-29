using DocumentsService.Data;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace DocumentsService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly IElasticSearchRepository _repository;

    public DocumentsController(IElasticRepositoryFactory factory)
    {
        const string indexName = "my-documents";

        _repository = factory.Create(indexName);
        _repository.CreateIndexIfNotExists(indexName);
    }

    [HttpPost("create-single")]
    public async Task<IActionResult> CreateSingle([FromBody] dynamic document)
    {
        document ??= new { Id = 1, Name = "Name 001" };

        await _repository.AddOrUpdate<dynamic>(document);

        return Ok();
    }

    [HttpPost("create-multiple")]
    public async Task<IActionResult> CreateMultiple([FromBody] IEnumerable<dynamic> documents)
    {
        documents ??= new List<dynamic>
        {
            new { id = 2, name = "Name 002" },
            new { id = 3, name = "Name 003" }
        };

        await _repository.AddOrUpdateBulk<dynamic>(documents);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingle(int id)
    {
        var document = await _repository.Get<dynamic>(id.ToString());

        if (document is null)
            return NotFound();

        return Ok(document);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var documents = await _repository.GetAll<dynamic>();

        return Ok(documents);
    }

    [HttpGet("{field}/{value}")]
    public async Task<IActionResult> GetByQuery(string field, string value)
    {
        field ??= "Name";
        value ??= "Name 002";

        var query = new TermQuery { Field = field, Value = value };
        var documents = await _repository.Query<dynamic>(query);

        if (documents?.Any() != true)
            return NotFound();

        return Ok(documents);
    }

    [HttpPost("remove/{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        await _repository.Remove<dynamic>(id.ToString());

        return Ok();
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveAll()
    {
        await _repository.RemoveAll<dynamic>();

        return Ok();
    }
}
