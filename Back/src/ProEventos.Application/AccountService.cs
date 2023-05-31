using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contratos;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class AccountService : IAccountService
    {
        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUserPersist userPersist)
        {
            _UserManager = userManager;
            _SignInManager = signInManager;
            _Mapper = mapper;
            _UserPersist = userPersist;
        }

        public UserManager<User> _UserManager { get; }
        public SignInManager<User> _SignInManager { get; }
        public IMapper _Mapper { get; }
        public IUserPersist _UserPersist { get; }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password)
        {
            try
            {
                var user = await _UserManager.Users.SingleOrDefaultAsync(user => user.UserName == userUpdateDto.Username.ToLower());

                return await _SignInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar password. Erro: {ex.Message}");
            }
        }

        public async Task<UserDto> CreateAccountAsync(UserDto userDto)
        {
            try
            {
                var user = _Mapper.Map<User>(userDto);

                var result = await _UserManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded)
                {
                    var userToReturn = _Mapper.Map<UserDto>(user);
                    return userToReturn;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _UserPersist.GetUserByUsernameAsync(username);
                if (user == null) return null;

                var userUpdateDto = _Mapper.Map<UserUpdateDto>(user);

                return userUpdateDto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<UserUpdateDto> UpdateAccountAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _UserPersist.GetUserByUsernameAsync(userUpdateDto.Username);
                if (user == null) return null;

                _Mapper.Map(userUpdateDto, user);

                var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
                var result = await _UserManager.ResetPasswordAsync(user, token, userUpdateDto.Password);

                _UserPersist.Update<User>(user);

                if (await _UserPersist.SaveChangesAsync())
                {
                    var userReturn = await _UserPersist.GetUserByUsernameAsync(user.UserName);

                    return _Mapper.Map<UserUpdateDto>(userReturn);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar usuário. Erro: {ex.Message}");
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _UserManager.Users.AnyAsync(user => user.UserName == username.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao verificar se usuário existe. Erro: {ex.Message}");
            }
        }
    }
}
