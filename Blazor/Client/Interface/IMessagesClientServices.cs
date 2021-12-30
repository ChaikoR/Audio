using Blazor.Shared.Models;

namespace Blazor.Client.Interface
{
        public interface IMessagesClientServices
        {
            Task<IEnumerable<Messages>> GetAllMessagesAsync();
            Task<Messages> CreateOrUpdatePost(Messages newMessages);
        }
}
