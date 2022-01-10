using Blazor.Server.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Shared.Models;

namespace Blazor.Server.Controllers
{
    [Route("api/Messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesServices _servicesGrpc;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MessagesController(IMessagesServices servicesGrpc, IWebHostEnvironment hostingEnvironment)
        {
            _servicesGrpc = servicesGrpc;
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        public async Task<ActionResult> GetMessages()
        {
            try
            {
                var model = await _servicesGrpc.GetAllMessagesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        [Route("CreateOrUpdatePost")]
        public async Task<IActionResult> CreateOrUpdatePost(Messages messages)
        {
            Messages newModel = new Messages();

            if (messages.BinaryData!=null && BitConverter.ToInt32(messages.BinaryData, 0) == 1) 
            {
                var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Files");
                var filePath = Path.Combine(uploads, "sound.ogg");

                    messages.BinaryData = System.IO.File.ReadAllBytes(filePath);

            }

            if (messages.MessagesId == 0)
            {
                //добавляем в БД
                newModel = await _servicesGrpc.AddMessageAsync(messages);
            }
            else {
                //обновляем
                newModel = await _servicesGrpc.UpdateMessageAsync(messages);
            }
            return Ok(newModel);

        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(IFormFile file)
        {

            if (file.ContentType != "audio/ogg; codecs=opus")
            {
                return BadRequest("Wrong file type");
            }

            var uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "Files");//uploads where you want to save data inside wwwroot

            //var filePath = Path.Combine(uploads, Path.GetRandomFileName() + ".opus");
            var filePath = Path.Combine(uploads, file.FileName + ".ogg");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok("File uploaded successfully");

        }

        [HttpDelete]
        [Route("DeleteMessage/{id:int}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            Messages delModel = await _servicesGrpc.DeleteMessageAsync(id);

            return Ok(delModel);

        }


        [HttpDelete]
        [Route("DeleteAudioFile/{id:int}")]
        public async Task<IActionResult> DeleteAudioFile(int id)
        {
            await _servicesGrpc.DeleteAudioFileAsync(id);
            return Ok();
        }
    }
}
