using Blazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Server.Interface
{
    public interface IMessagesServices
    {
        Task<List<Messages>> GetAllMessagesAsync();
    }
}
