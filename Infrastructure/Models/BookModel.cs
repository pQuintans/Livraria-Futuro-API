using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaFuturo.Infrastructure.Models
{
    public class BookModel
    {
        public BookModel(int id, string name, string author, int pageTotal, bool isActive, string? category)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.category = category;
            this.pageTotal = pageTotal;
            this.isActive = isActive;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public string? category { get; set; }
        public int pageTotal { get; set; }
        public bool isActive { get; set; }
    }
}
