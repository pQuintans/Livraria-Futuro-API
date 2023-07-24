namespace LivrariaFuturo.API.Models.Requests
{
    public class UserCreationRequest
    {
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
