using Application.Dto;
using Application.Dto.AuthDto;
using Application.Interfaces.IRepo;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace EmployeePortal.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo authRepo;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthService(IAuthRepo authRepo, IHttpContextAccessor httpContextAccessor)
        {
            this.authRepo = authRepo;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Username))
                return new LoginResponseDto { Success = false ,Message="Username is required"};

            if (string.IsNullOrWhiteSpace(loginDto.Password))
                return new LoginResponseDto { Success = false, Message = "Password is required" };

            var user = await authRepo.GetByUsernameAsync(loginDto.Username);

            if (user == null)
                return new LoginResponseDto { Success = false, Message = "User not found" };
            if (user.PasswordHash != loginDto.Password)
                return new LoginResponseDto { Success = false, Message = "Invalid password" };


            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful",
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role?.RoleName ?? ""

                }
            };
        }
    }
}
