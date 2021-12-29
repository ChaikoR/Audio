using Grpc.Core;
using GrpcService.Interface;

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
    }
}
