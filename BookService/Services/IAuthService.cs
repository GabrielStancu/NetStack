using BookService.Data.Models;

namespace BookService.Services;

public interface IAuthService
{
    Task<(int, string)> Register(RegisterModel model, string role);
    Task<(int, string)> Login(LoginModel model);
}
