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

        public async Task<Messages> UpdateMessageAsync(Messages model)
        {
            Messages? updateModel = new Messages();
            updateModel = await _context.Messages.FindAsync(model.MessagesId);
            updateModel.MessagesId = model.MessagesId;
            updateModel.Name = model.Name;
            
            return updateModel;
        }


        public async Task<Messages> DeleteMessageAsync(int id)
        {
            Messages? deleteModel = new Messages();
            deleteModel = await _context.Messages.FindAsync(id);
            if (deleteModel != null) { 
                _context.Messages.Remove(deleteModel);
                _context.SaveChanges();
                return deleteModel;
            }
            return null;
        }
    }
}
