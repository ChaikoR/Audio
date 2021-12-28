using Blazor.Server.Interface;
using Blazor.Shared.Models;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public class MessagesServices : IMessagesServices
    {
        public async Task<List<Messages>> GetAllMessagesAsync()
        {

            List<Messages> messages = new List<Messages>();

            // //получаем Grpc данные
            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);



            //// var reply = client.GetMessages(new MessagesRequest());

            using (var call = client.GetMessages(new MessagesRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;
                    messages.Add(new Messages
                    {
                        MessagesId = currentCustomer.MessagesId,
                        Name = currentCustomer.Name
                    });
                }
            }

                return messages;
        }
    }
}
