using System.Text.Json;
using FoodService.MessageBrokerLibrary;
using FoodService.Models;
using FoodService.Models.Customs;
using FoodService.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FoodService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly IFoodRepository _foodRepository;
    private readonly IPublisher _publisher;

    public FoodController(IFoodRepository foodRepository, IPublisher publisher)
    {
        _foodRepository = foodRepository;
        _publisher = publisher;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var food = await _foodRepository.GetAll();
        if (food?.Any() != true)
        {
            return NotFound();
        }

        return Ok(food);
    }

    [HttpGet("GetByCategory")]
    public async Task<IActionResult> GetByCategory(string categoryCode)
    {
        var food = await _foodRepository.GetByCategory(categoryCode);
        if (food?.Any() != true)
        {
            return NotFound();
        }

        return Ok(food);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var food = await _foodRepository.Get(id);

        if (food == null)
        {
            return NotFound();
        }

        return Ok(food);
    }

    [HttpPost("Save")]
    public async Task<IActionResult> Save([FromForm] FoodModel model)
    {
        var food = new Food
        {
            Id = string.IsNullOrEmpty(model.Id) ? ObjectId.GenerateNewId().ToString() : model.Id,
            CategoryCode = model.CategoryCode,
            Name = model.Name,
            Description = model.Description,
            Size = model.Size,
            Discount = model.Discount,
            Price = model.Price,
            Image = GetImage(model.Image),
            Deleted = model.Deleted,
        };

        await _foodRepository.Save(food);

        if (string.IsNullOrEmpty(model.Id))
        {
            _publisher.Publish(JsonSerializer.Serialize(food), "order.food", null);
            return StatusCode(201);
        }
        else
        {
            return NoContent();
        }
    }

    [HttpDelete("Delete")]
    public async Task<IActionResult> Delete(string id)
    {
        await _foodRepository.Delete(id);
        return NoContent();
    }

    [HttpDelete("SoftDelete")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        await _foodRepository.SoftDelete(id);
        return NoContent();
    }

    private static byte[] GetImage(IFormFile image)
    {
        if (image.Length > 0)
        {
            using var ms = new MemoryStream();
            image.CopyTo(ms);

            return ms.ToArray();
        }
        else
        {
            return default!;
        }
    }
}
