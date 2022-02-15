using OnePlan.Business.Dto.Auth;
using OnePlan.Business.ViewModels;
namespace OnePlan.Core.Services.Interfaces;

public interface IAuthService
{
    Task<LoginViewModel> Login(LoginDto loginDto);
}
