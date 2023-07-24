using LivrariaFuturo.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;

namespace LivrariaFuturo.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetByUsername(string username);
        Task<bool> Save(string email, string name, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IMemoryCache _cache;
        private readonly string cacheKey = "users";

        public UserRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<UserModel?> GetByUsername(string username)
        {
            List<UserModel> users;
            _cache.TryGetValue<List<UserModel>>(cacheKey, out users);

            if(users == null) return null;

            UserModel? user = users.Where(u => u.email == username).FirstOrDefault();

            return user;
        }

        public async Task<bool> Save(string email, string name, string password)
        {
            List<UserModel> users;
            bool userAlreadyExists = false;
            _cache.TryGetValue<List<UserModel>>(cacheKey, out users);

            if (users == null) users = new List<UserModel>();

            userAlreadyExists = users?.Where(u => u.email == email).FirstOrDefault() != null;

            if (userAlreadyExists) return false;

            var id = users.Count() > 0 ? users.Last().id + 1 : 1;
            UserModel user = new UserModel(id, name, email, password);
            users.Add(user);

            _cache.Set(cacheKey, users);
            return true;
        }
    }
}
