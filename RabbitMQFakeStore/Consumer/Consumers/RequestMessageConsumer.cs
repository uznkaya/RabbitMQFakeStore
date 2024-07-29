using MassTransit;
using Shared;
using Shared.RequestResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        private readonly ApplicationDbContext _context;
        public RequestMessageConsumer(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {

            var message = context.Message;

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("https://fakestoreapi.com/products", new
            {
                message.Title,
                message.Price,
                message.Description,
                message.Image,
                message.Category
            });
            if (response.IsSuccessStatusCode)
            {
                var entity = await _context.RequestMessages.FindAsync(message.Id);
                if(entity != null)
                {
                    entity.IsSucceeded = true;
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
