using Blazor.Client.Interface;
using Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.Client.Services
{
    
    public class MessagesClientServices : IMessagesClientServices
    {
        private readonly HttpClient _httpClient;
        public MessagesClientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Messages>> GetAllMessagesAsync()
        {
            
            return await _httpClient.GetFromJsonAsync<IEnumerable<Messages>>("/api/Messages");
        }

        public async Task<Messages> CreateOrUpdateMessageAsync(Messages newMessages, int safeFile)
        {
            if (safeFile == 1) {
                newMessages.BinaryData = BitConverter.GetBytes(safeFile);
            }
             var response = await _httpClient.PostAsJsonAsync<Messages>("/api/Messages/CreateOrUpdatePost", newMessages);
             return await response.Content.ReadFromJsonAsync<Messages>();

        }

        public async Task<Messages> DeleteMessageAsync(int id)
        {
            //var response = await _httpClient.PostAsJsonAsync<int>("/api/Messages/DeleteMessage", id);
            var response = await _httpClient.DeleteAsync($"/api/Messages/DeleteMessage/{id}");
            return await response.Content.ReadFromJsonAsync<Messages>();
        }
    }
}
