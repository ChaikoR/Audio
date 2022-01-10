using Blazor.Server.Interface;
using Blazor.Shared.Models;
using Google.Protobuf;
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

            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = client.GetMessages(new MessagesRequest(), new Grpc.Core.Metadata());

            foreach (var item in result.Messages)
            {
                messages.Add(new Messages
                    {
                        MessagesId = item.MessagesId,
                        Name = item.Name,
                        BinaryData = item.BinaryData.ToByteArray(),
                    }
                );
            }
                
            return messages;
        }

        public async Task<Messages> AddMessageAsync(Messages model)
        {
            MessageModel messageModel = new MessageModel();
            messageModel.Name = model.Name;
            if (model.BinaryData != null) {
                messageModel.BinaryData = ByteString.CopyFrom(model.BinaryData);
            }
           

            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.AddMessageAsync(messageModel);

            model.MessagesId = result.MessagesId;
            model.Name = result.Name;
            //model.BinaryData = result.BinaryData;

            return model;
        }

        public async Task<Messages> UpdateMessageAsync(Messages model)
        {
            MessageModel messageModel = new MessageModel();
            messageModel.MessagesId = model.MessagesId;
            messageModel.Name = model.Name;

            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.UpdateMessageAsync(messageModel);

            model.MessagesId = result.MessagesId;
            model.Name = result.Name;
            return model;
        }

        public async Task<Messages> DeleteMessageAsync(int id) {
            
            MessageId messageId = new MessageId();
            messageId.MessagesId = id;

            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.DeleteMessageAsync(messageId);

            Messages delModel = new Messages();
            delModel.MessagesId=result.MessagesId;
            delModel.Name = result.Name;    
            return delModel;
        }

        public async Task DeleteAudioFileAsync(int id) {

            MessageId messageId = new MessageId();
            messageId.MessagesId = id;

            var channel = GrpcChannel.ForAddress("https://localhost:7298");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.DeleteAudioFileAsync(messageId);
        }
    }
}
