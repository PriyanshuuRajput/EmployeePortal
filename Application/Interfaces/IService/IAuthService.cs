using Application.Dto.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IService
{
    public interface IAuthService
    {
       Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    }
}
