using Blazor.Shared.Models;

namespace Blazor.Client.Interface
{
        public interface IMessagesClientServices
        {
            Task<IEnumerable<Messages>> GetAllMessagesAsync();
            Task<Messages> CreateOrUpdateMessageAsync(Messages newMessages, int safeFile);
            Task<Messages> DeleteMessageAsync(int Id);
    }
}
