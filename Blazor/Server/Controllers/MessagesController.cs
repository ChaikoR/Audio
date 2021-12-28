using Blazor.Server.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesServices _servicesGrpc;
        public MessagesController(IMessagesServices servicesGrpc)
        {
            _servicesGrpc = servicesGrpc;
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
    }
}
