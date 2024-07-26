using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestResponseMessages
{
    public class RequestMessage
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
        public bool? IsSucceeded { get; set; }


    }
}
