using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestResponseMessageModel.Product
{
    public class Product: BaseModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public bool? IsSucceeded { get; set; } = false;
    }
}
