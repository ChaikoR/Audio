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

            //var channel = GrpcChannel.ForAddress("https://localhost:7298");
            //var client = new People.PeopleClient(channel);
            //var result = client.GetPeople(new RequestModel(), new Grpc.Core.Metadata());

            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = client.GetMessages(new MessagesRequest(), new Grpc.Core.Metadata());


            foreach (var item in result.Messages)
            {
                messages.Add(new Messages
                    {
                        MessagesId = item.MessagesId,
                        Name = item.Name,
                    }
                );
            }
                
            return messages;
        }
    }
}
