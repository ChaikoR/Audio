using GrpcService.Models;

namespace GrpcService.Interface
{
    public interface IMessagesServices
    {
        Task<List<Messages>> GetAllAsync();
        
        Messages GetMessagesById(int id);
        void UpdateMessages(Messages model);
        void DeleteMessages(int id);
        Task<Messages> AddMessageAsync(Messages model);

        //Task<MessageModel> FindMessageAsync(int messageId);
    }
}
