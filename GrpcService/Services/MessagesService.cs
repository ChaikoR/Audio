using Grpc.Core;
using GrpcService.Interface;
using GrpcService.Models;

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

            var dbModel = await _context.GetAllAsync();
            
                foreach (var message in dbModel)
                {
                    messages.Add(new MessageModel 
                    {
                        MessagesId=message.MessagesId,
                        Name=message.Name
                    });
                }
            MessagesList replyModel = new MessagesList();
            replyModel.Messages.AddRange(messages);
            return replyModel;
        }

        public override async Task<MessageModel> AddMessage(MessageModel request, ServerCallContext context)
        {
            Messages addModel = new Messages();
            addModel.Name = request.Name;
            Messages updateMessage = await _context.AddMessageAsync(addModel);
            if (updateMessage == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Contact with ID={request.MessagesId} is not found."));
            }
            MessageModel messageModel = new MessageModel();
            messageModel.MessagesId = updateMessage.MessagesId;
            messageModel.Name = updateMessage.Name;

            return await Task.FromResult(messageModel);
        }
    }
}
