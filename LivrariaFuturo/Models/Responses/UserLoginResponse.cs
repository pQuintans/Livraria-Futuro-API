using LivrariaFuturo.Infrastructure.Models;

namespace LivrariaFuturo.API.Models.Requests
{
    public class UserLoginResponse
    {
        public UserLoginResponse(UserModel user, string token)
        {
            this.id = user.id;
            this.name = user.name;
            this.email = user.email;
            this.token = token;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string token { get; set; }
    }
}
