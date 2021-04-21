using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace nyssKursovoyReact.Controllers
{
    

    [ApiController]
    [Route("[controller]")]
    public class DecryptController : ControllerBase
    {
        public static string test = "start";

        private readonly ILogger<DecryptController> _logger;

        public DecryptController(ILogger<DecryptController> logger)
        {
            _logger = logger;
        }

       
        [HttpPost]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> Post([FromBody] FileModel text)
        {
           
            string jsonRequest = JsonSerializer.Serialize(text);
            



            return Ok(jsonRequest);
           
        }
        /*[HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Post([FromRoute] Guid id, [FromForm] IFormFile body)
        {
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await body.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            var filename = body.FileName;
            var contentType = body.ContentType;



            return Ok(filename + " "+ contentType);
        }*/
    }
}
