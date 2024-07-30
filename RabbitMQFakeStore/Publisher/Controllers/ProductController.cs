using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.RequestResponseMessageModel.Product;
using Shared.RequestResponseMessages;
using Shared.RequestViewModel.Product;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IRequestClient<Product> _client;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(IRequestClient<Product> client, ApplicationDbContext context, IMapper mapper)
        {
            _client = client;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductVM createProductVM)
        {
            var newProduct = _mapper.Map<Product>(createProductVM);
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            var response = await _client.GetResponse<ResponseMessage>(newProduct);
            return Ok(response);
        }

        /*
        [HttpGet]
        public async Task<IActionResult> GetProductAll()
        {
            // Elma, Kalem, PC, Sakız
            // Elmayı aldı queue ya gönderdi. queue consumer kısmı kontrol edicek. Gelen veri doğru mu diye eğer doğru ise products listesine ekle
            // products listesini döndür.
        }
        */

    }
}
