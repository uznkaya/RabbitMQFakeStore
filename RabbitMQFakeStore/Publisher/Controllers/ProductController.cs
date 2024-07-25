using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.RequestResponseMessages;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IRequestClient<RequestMessage> _client;

        public ProductController(IRequestClient<RequestMessage> client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(RequestMessage requestMessage)
        {
            var response = await _client.GetResponse<ResponseMessage>(requestMessage);
            return Ok(response);
        }

    }
}
