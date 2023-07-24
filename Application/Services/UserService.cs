using LivrariaFuturo.Core.Helpers;
using LivrariaFuturo.Infrastructure.Models;
using LivrariaFuturo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaFuturo.Application.Services
{
    public interface IUserService
    {
        Task<UserModel?> GetByUsername(string username);
        Task<bool> Save(string email, string name, string password);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<UserModel?> GetByUsername(string username)
        {
            return await this._userRepository.GetByUsername(username);
        }

        public async Task<bool> Save(string email, string name, string password)
        {
            var hashedPassword = password.ToMd5Hash();

            return await this._userRepository.Save(email, name, hashedPassword);
        }
    }
}
