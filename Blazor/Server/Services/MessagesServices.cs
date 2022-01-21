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

            var channel = GrpcChannel.ForAddress("https://localhost:2222/");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.GetMessagesAsync(new MessagesRequest(), new Grpc.Core.Metadata());

            foreach (var item in result.Messages)
            {
               byte[]? bData;
                if (item.BinaryData == ByteString.Empty)
                {
                    bData = null;
                }
                else
                {
                    bData = item.BinaryData.ToByteArray();
                }
                messages.Add(new Messages
                    {
                        MessagesId = item.MessagesId,
                        Name = item.Name,
                        BinaryData = bData,
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
           

            var channel = GrpcChannel.ForAddress("https://localhost:2222");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.AddMessageAsync(messageModel);

            model.MessagesId = result.MessagesId;
            model.Name = result.Name;
            model.BinaryData = result.BinaryData.ToByteArray();

            return model;
        }

        public async Task<Messages> UpdateMessageAsync(Messages model)
        {
            MessageModel messageModel = new();
            messageModel.MessagesId = model.MessagesId;
            messageModel.Name = model.Name;
            if (model.BinaryData != null)
            {
                messageModel.BinaryData = ByteString.CopyFrom(model.BinaryData);
            }

            var channel = GrpcChannel.ForAddress("https://localhost:2222");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.UpdateMessageAsync(messageModel);

            model.MessagesId = result.MessagesId;
            model.Name = result.Name;
            model.BinaryData = result.BinaryData.ToByteArray();
            return model;
        }

        public async Task<Messages> DeleteMessageAsync(int id) {
            
            MessageId messageId = new();
            messageId.MessagesId = id;

            var channel = GrpcChannel.ForAddress("https://localhost:2222");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.DeleteMessageAsync(messageId);

            Messages delModel = new();
            delModel.MessagesId=result.MessagesId;
            delModel.Name = result.Name;
            delModel.BinaryData = result.BinaryData.ToByteArray();

            return delModel;
        }

        public async Task<Messages> DeleteAudioFileAsync(int id) {

            MessageId messageId = new();
            messageId.MessagesId = id;

            var channel = GrpcChannel.ForAddress("https://localhost:2222");
            var client = new RemoteMessages.RemoteMessagesClient(channel);
            var result = await client.DeleteAudioFileAsync(messageId);
            
            Messages delModel = new();
            delModel.MessagesId = result.MessagesId;
            delModel.Name = result.Name;
            delModel.BinaryData = result.BinaryData.ToByteArray();

            return delModel;
        }
    }
}
