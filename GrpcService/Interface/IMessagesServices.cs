using GrpcService.Models;

namespace GrpcService.Interface
{
    public interface IMessagesServices
    {
        Task<List<Messages>> GetAllAsync();
        Task<Messages> AddMessageAsync(Messages model);
        Task<Messages> UpdateMessageAsync(Messages model);
        Task<Messages> DeleteMessageAsync(int id);
    }
}
