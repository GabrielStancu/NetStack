using FoodService.Models;
using FoodService.Models.Customs;
using FoodService.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FoodService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodCategoryController : ControllerBase
{
    private readonly IFoodCategoryRepository _foodCategoryRepository;

    public FoodCategoryController(IFoodCategoryRepository foodCategoryRepository)
    {
        _foodCategoryRepository = foodCategoryRepository;
    }

    [HttpGet(nameof(GetAll))]
    public async Task<IActionResult> GetAll()
    {
        var foodCategories = await _foodCategoryRepository.GetAll();

        if (foodCategories?.Any() != true)
            return NotFound();

        return Ok(foodCategories);
    }

    [HttpPost(nameof(Save))]
    public async Task<IActionResult> Save([FromBody] FoodCategoryModel model)
    {
        var foodCategory = new FoodCategory
        {
            Id = string.IsNullOrEmpty(model.Id) ? ObjectId.GenerateNewId().ToString() : model.Id,
            Code = model.Code,
            Name = model.Name,
            Deleted = model.Deleted,
        };

        await _foodCategoryRepository.Save(foodCategory);

        if (string.IsNullOrEmpty(model.Id))
        {
            return StatusCode(201);
        }
        else
        {
            return NoContent();
        }
    }

    [HttpDelete(nameof(Delete))]
    public async Task<IActionResult> Delete(string id)
    {
        await _foodCategoryRepository.Delete(id);
        return NoContent();
    }

    [HttpDelete(nameof(SoftDelete))]
    public async Task<IActionResult> SoftDelete(string id)
    {
        await _foodCategoryRepository.SoftDelete(id);
        return NoContent();
    }
}
