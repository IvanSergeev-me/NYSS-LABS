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
        //public static string test = "start";

        private readonly ILogger<DecryptController> _logger;

        public DecryptController(ILogger<DecryptController> logger)
        {
            _logger = logger;
        }

       
        [HttpPost]
        //[Route("{id:Guid}")]
        public async Task<IActionResult> Post([FromBody] FileModel body)
        {
            body.Decrypted = Decrypt(body.Text, body.Key);
            string jsonRequest = JsonSerializer.Serialize(body);
            



            return Ok(jsonRequest);
           
        }

        private string Decrypt(string text, string key)
        {

            string alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key)) return "Все круто! Но, пожалуйста, введите ключ...";
            key = key.ToUpper();
            int position = 0;
            string retValue = "";
            foreach (char symbol in text)
            {
                if (alphabet.Contains(symbol.ToString().ToUpper()))
                {
                    bool isUpper = char.IsUpper(symbol);
                    char letter = Char.ToUpper(symbol);
                    int charPosition = alphabet.IndexOf(letter);
                    int keyPosition = alphabet.IndexOf(key[position]);
                    int dectyptedCharPosition = charPosition - keyPosition;
                    if (dectyptedCharPosition < 0) dectyptedCharPosition += 33;
                    if (isUpper) retValue += alphabet[dectyptedCharPosition];
                    else retValue += char.ToLower(alphabet[dectyptedCharPosition]);
                    //Покрываем все буквы текста ключем,
                    //Когда символы ключа заканчиваются покрытие участка текста начинается с нулевого символа ключа
                    position++;
                    if (position >= key.Length) position = 0;
                }
                else retValue += symbol;
            }
            return retValue;

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
