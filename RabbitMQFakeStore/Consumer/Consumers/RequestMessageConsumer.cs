using MassTransit;
using Shared.RequestResponseMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.Consumers
{
    public class RequestMessageConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
           Task.Delay(5000).Wait();
           await context.RespondAsync<ResponseMessage>(new() { Text = "başarılı" });
        }
    }
}
