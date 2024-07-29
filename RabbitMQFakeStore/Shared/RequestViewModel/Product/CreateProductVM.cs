using System;
namespace Shared.RequestViewModel.Product
{
    public class CreateProductVM
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
    }
}

