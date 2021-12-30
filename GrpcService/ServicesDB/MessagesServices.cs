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

        public async Task<List<Messages>> GetAllAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Messages> AddMessageAsync(Messages model)
        {
            _context.Messages.Add(model);
            await _context.SaveChangesAsync();
            return model;
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

        //public async Task<MessageModel> FindMessageAsync(int messageId)
        //{
        //    return await _context.Messages.FirstOrDefaultAsync(i => i.MessagesId == messageId);
        //}
    }
}
