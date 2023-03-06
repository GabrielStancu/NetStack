using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Models.Custom;
using UserService.Repositories;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCustomerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserCustomerController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost(nameof(PostUserCustomer))]
    public async Task<IActionResult> PostUserCustomer([FromBody] UserCustomerModel model)
    {
        var response = new ResponseModel<UserCustomerModel>();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = model.Username,
            Password = model.Password
        };

        var customer = new Customer
        {
            UserId = user.Id,
            Name = model.Name,
            Address = model.Address,
            Phone = model.Phone,
            Point = 0
        };

        await _unitOfWork.UserRepository.Save(user);
        await _unitOfWork.CustomerRepository.Save(customer);

        bool result = await _unitOfWork.SaveAsync();

        if (result)
        {
            model.Id = user.Id;
            response.Data = model;

            return StatusCode(201, response);
        }
        else
        {
            throw new Exception("Error while saving the user");
        }
    }

    public async Task<IActionResult> GetCustomerByUserId(Guid userId)
    {
        var response = new ResponseModel<CustomerModel>();

        var customer = await _unitOfWork.CustomerRepository.GetByUserId(userId);

        if (customer is null)
        {
            response.ErrorMessage = $"Not found. User {userId} does not exist";
            return NotFound(response);
        }
        else
        {
            response.Data = new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                UserId = customer.UserId,
                Address = customer.Address,
                Phone = customer.Phone,
                Point = customer.Point
            };
            return Ok(response);
        }
    }

    [HttpGet(nameof(GetAllUserCustomer))]
    public async Task<IActionResult> GetAllUserCustomer()
    {
        var response = new ResponseModel<List<UserCustomerModel>>();
        var data = await _unitOfWork.CustomerRepository.GetAll();

        if (data is null)
        {
            response.ErrorMessage = "List empty";
            return NotFound(response);
        }

        var userCustomers = new List<UserCustomerModel>();

        foreach (var item in data)
        {
            userCustomers.Add(new UserCustomerModel
            {
                Id = item.User.Id,
                Username = item.User.Username,
                Name = item.Name,
                Address = item.Address,
                Point = item.Point,
                Phone = item.Phone
            });
        }

        response.Data = userCustomers;

        return Ok(response);
    }

    [HttpPut(nameof(PutUserCustomer))]
    public async Task<IActionResult> PutUserCustomer([FromBody] UserCustomerModel model)
    {
        var response = new ResponseModel<UserCustomerModel>();
        var user = await _unitOfWork.UserRepository.GetById(model.Id);
        var customer = await _unitOfWork.CustomerRepository.GetByUserId(model.Id);

        if (user is null || customer is null)
        {
            response.ErrorMessage = $"Data {model.Id} Not Found";
            return NotFound(response);
        }

        user.Password = model.Password;

        customer.Name = model.Name;
        customer.Address = model.Address;
        customer.Phone = model.Phone;
        customer.Point = model.Point;

        await _unitOfWork.UserRepository.Save(user);
        await _unitOfWork.CustomerRepository.Save(customer);

        bool result = await _unitOfWork.SaveAsync();

        if (result)
        {
            model.Id = user.Id;
            response.Data = model;

            return NoContent();
        }
        else
        {
            throw new Exception("Error while creating the user");
        }
    }

    [HttpDelete(nameof(DeleteUserCustomerByUserId))]
    public async Task<IActionResult> DeleteUserCustomerByUserId(Guid userId)
    {
        await _unitOfWork.UserRepository.SoftDelete(userId);
        await _unitOfWork.CustomerRepository.SoftDelete(userId);

        bool result = await _unitOfWork.SaveAsync();

        if (result)
        {
            return NoContent();
        }
        else
        {
            throw new Exception("Could not find user to be deleted");
        }
    }
}
