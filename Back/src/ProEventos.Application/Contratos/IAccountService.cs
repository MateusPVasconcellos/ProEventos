using Microsoft.AspNetCore.Identity;
using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateDto> GetUserByUsernameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);
        Task<UserDto> CreateAccountAsync(UserDto userDto);
        Task<UserUpdateDto> UpdateAccountAsync(UserUpdateDto userUpdateDto);

    }
}
