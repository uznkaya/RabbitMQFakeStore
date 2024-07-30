using AutoMapper;
using Consumer.ExternalServices;
using MassTransit;
using Shared;
using Shared.RequestResponseMessageModel.Product;
using Shared.RequestResponseMessages;
using Shared.RequestViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class CreateProductConsumer : IConsumer<Product>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly FakeStoreService _storeService;

        public CreateProductConsumer(ApplicationDbContext context, IMapper mapper, FakeStoreService storeService)
        {
            _context = context;
            _mapper = mapper;
            _storeService = storeService;
        }
        public async Task Consume(ConsumeContext<Product> context)
        {

            var message = context.Message;
            var newProduct = _mapper.Map<CreateProductVM>(message);

           var response = await _storeService.CreateproductRequest(newProduct);

            if (response.IsSuccessStatusCode)
            {
                var entity = await _context.Products.FindAsync(message.Id);
                if(entity != null)
                {
                    entity.IsSucceeded = true;
                    entity.UpdatedDate = DateTime.Now;
                    await _context.SaveChangesAsync();

                    await context.RespondAsync<ResponseMessage>(new()
                    {
                        Text = $"{message.Title} adlı ürün başarıyla kaydedilmiştir."
                    });

                    Console.WriteLine($"{message.Title} adlı ürün başarıyla kaydedilmiştir.");
                }
                else
                {
                    await context.RespondAsync<ResponseMessage>(new()
                    {
                        Text = $"{message.Title} adlı ürün bulunamadı"
                    });
                }
            }
            else
            {
                await context.RespondAsync<ResponseMessage>(new()
                {
                    Text = $"{message.Title} adlı üründe hata oluştu."
                });

                Console.WriteLine($"{message.Title} adlı üründe hata oluştu.");
            }
        }
    }
}
