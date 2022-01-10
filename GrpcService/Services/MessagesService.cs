using Google.Protobuf;
using Grpc.Core;
using GrpcService.Interface;
using GrpcService.Models;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GrpcService.Services
{
    public class MessagesService : RemoteMessages.RemoteMessagesBase
    {
        private readonly ILogger<MessagesService> _loger;
        private readonly IMessagesServices _context;
        public MessagesService(ILogger<MessagesService> loger, IMessagesServices context)
        {
            _loger = loger;
            _context = context;
        }

        public override async Task<MessagesList> GetMessages(MessagesRequest request, ServerCallContext context)
        {
            List<MessageModel> messages = new List<MessageModel>();

            List<Messages> dbModel = await _context.GetAllAsync();

            foreach (var message in dbModel)
            {
                //Byte[]? binaryData =null;
                //if (message.BinaryData != null)
                //{
                //    binaryData = ByteToByteString(message.BinaryData);
                //}
                
                messages.Add(new MessageModel
                {
                    MessagesId = message.MessagesId,
                    Name = message.Name,
                    BinaryData = message.BinaryData != null ? ByteToByteString(message.BinaryData) : ByteToByteString(null),
                });
            }
            //messages = dbModel.Select(x => new MessageModel
            //{
            //    MessagesId = x.MessagesId,
            //    Name = x.Name,
            //    BinaryData = x?.BinaryData : null, ByteToByteString(x.BinaryData)
            //}).ToList(); 

            MessagesList replyModel = new MessagesList();
            replyModel.Messages.AddRange(messages);

            return replyModel;
        }

        public override async Task<MessageModel> AddMessage(MessageModel request, ServerCallContext context)
        {
            Messages addModel = new Messages();
            addModel.Name = request.Name;
            if (request.BinaryData != null) {
                addModel.BinaryData = ByteStringToByte(request.BinaryData);
            }
            

            Messages addMessage = await _context.AddMessageAsync(addModel);
            if (addMessage == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Contact with ID={request.MessagesId} is not found."));
            }
            MessageModel messageModel = new MessageModel();
            messageModel.MessagesId = addMessage.MessagesId;
            messageModel.Name = addMessage.Name;

            return await Task.FromResult(messageModel);
        }
        public override async Task<MessageModel> UpdateMessage(MessageModel request, ServerCallContext context)
        {
            Messages updateModel = new Messages();
            updateModel.MessagesId=request.MessagesId;
            updateModel.Name = request.Name;

            Messages updateMessage = await _context.UpdateMessageAsync(updateModel);
            if (updateMessage == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Contact with ID={request.MessagesId} is not found."));
            }
            MessageModel messageModel = new MessageModel();
            messageModel.MessagesId = updateMessage.MessagesId;
            messageModel.Name = updateMessage.Name;

            return await Task.FromResult(messageModel);
        }

        public override async Task<MessageModel> DeleteMessage(MessageId request, ServerCallContext context)
        {
            var model = await _context.DeleteMessageAsync(request.MessagesId);
            
            MessageModel delModel = new MessageModel();
            delModel.MessagesId = model.MessagesId; 
            delModel.Name = model.Name;
            return await Task.FromResult(delModel);
        }

        public override async Task<MessagesRequest> DeleteAudioFile(MessageId request, ServerCallContext context)
        {
            await _context.DeleteAudioFileAsync(request.MessagesId);
            return await Task.FromResult(new MessagesRequest());
        }
        public byte[] ByteStringToByte(ByteString bytes) {
            byte[]  ret = bytes.ToByteArray();
            return ret;
        }

        public ByteString ByteToByteString(byte[]? arrBytes)
        {
            if (arrBytes != null) {
                return ByteString.CopyFrom(arrBytes);
            }
            return ByteString.CopyFrom(null);
        }
    }
}
