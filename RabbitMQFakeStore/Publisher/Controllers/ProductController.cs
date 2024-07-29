using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.RequestResponseMessages;
using Shared.RequestViewModel;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IRequestClient<RequestMessage> _client;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(IRequestClient<RequestMessage> client, ApplicationDbContext context, IMapper mapper)
        {
            _client = client;
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductVM createProductVM)
        {
            var newProduct = _mapper.Map<RequestMessage>(createProductVM);
            _context.RequestMessages.Add(newProduct);
            await _context.SaveChangesAsync();
            var response = await _client.GetResponse<ResponseMessage>(newProduct);
            return Ok(response);
        }
       

    }
}
