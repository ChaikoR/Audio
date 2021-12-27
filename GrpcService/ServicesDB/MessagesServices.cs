using GrpcService.Data;
using GrpcService.Interface;
using GrpcService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GrpcService.ServicesDB
{

    public class MessagesServices : IMessagesServices
    {
        private readonly MessagesDBConext _context;
        public MessagesServices(MessagesDBConext context)
        {
            _context = context;
        }

        async Task<List<Messages>> IMessagesServices.GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        void IMessagesServices.AddMessages(Messages model)
        {
            throw new NotImplementedException();
        }

        void IMessagesServices.DeleteMessages(int id)
        {
            throw new NotImplementedException();
        }

        Messages IMessagesServices.GetMessagesById(int id)
        {
            throw new NotImplementedException();
        }

        void IMessagesServices.UpdateMessages(Messages model)
        {
            throw new NotImplementedException();
        }
    }
}
