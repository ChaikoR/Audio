using Blazor.Server.Interface;
using Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Server.Services
{
    public class MessagesServices : IMessagesServices
    {
        public Task<List<Messages>> GetAllMessagesAsync()
        {
            //получаем Grpc данные
            throw new NotImplementedException();
        }
    }
}
