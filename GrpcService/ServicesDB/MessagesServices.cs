using GrpcService.Data;
using GrpcService.Interface;
using GrpcService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

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
            var model = await _context.Messages.ToListAsync();
            return model;
        }

        public async Task<Messages> AddMessageAsync(Messages model)
        {
            //model.BinaryData = model.BinaryData.;
            Messages messagesAdd = new Messages();

            messagesAdd.Name = model.Name;
            messagesAdd.BinaryData = model.BinaryData;

            _context.Messages.Add(messagesAdd);
            await _context.SaveChangesAsync();
            return messagesAdd;
        }

        public async Task<Messages> UpdateMessageAsync(Messages model)
        {
            Messages? updateModel = new Messages();
            updateModel = await _context.Messages.FindAsync(model.MessagesId);
            
            updateModel.MessagesId = model.MessagesId;
            updateModel.Name = model.Name;
            updateModel.BinaryData = model.BinaryData;

            await _context.SaveChangesAsync();



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

        public async Task DeleteAudioFileAsync(int id)
        {
            var model = _context.Messages.Find(id);
            if (model != null) {
                model.BinaryData = null;
                await _context.SaveChangesAsync(); 
            }
        }
    }
}
