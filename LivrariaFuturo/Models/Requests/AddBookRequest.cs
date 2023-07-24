namespace LivrariaFuturo.API.Models.Requests
{
    public class AddBookRequest
    {
        public string name { get; set; }
            
        public string author { get; set; }
            
        public string? category { get; set; }
            
        public int pageTotal { get; set; }
        public bool isActive { get; set; }
    }
}
