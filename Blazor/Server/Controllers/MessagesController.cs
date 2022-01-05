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
        [Route("Save/{id:int}")]
        public async Task<IActionResult> Save(IFormFile file, int id)
        {

            if (file.ContentType != "audio/ogg; codecs=opus")
            {
                return BadRequest("Wrong file type");
            }

            return Ok("File uploaded successfully");

        }

        [HttpDelete]
        [Route("DeleteMessage/{id:int}")]
        //[HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            Messages delModel = await _servicesGrpc.DeleteMessageAsync(id);

            return Ok(delModel);

        }
    }
}
