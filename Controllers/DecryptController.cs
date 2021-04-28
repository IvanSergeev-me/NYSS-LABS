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
       

        private readonly ILogger<DecryptController> _logger;

        public DecryptController(ILogger<DecryptController> logger)
        {
            _logger = logger;
        }

       
        [HttpPost]
       
        public async Task<IActionResult> Post([FromBody] FileModel body)
        {
            
            body.Decrypted = GetCrypted(body.Text, body.Key, body.CryptDirection);
            string jsonRequest = JsonSerializer.Serialize(body);
            



            return Ok(jsonRequest);
           
        }

        private string GetCrypted(string text, string key, bool cryptDirection)
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
                    char letter = char.ToUpper(symbol);
                    int charPosition = alphabet.IndexOf(letter);
                    int keyPosition = alphabet.IndexOf(key[position]);
                    int dectyptedCharPosition;
                    //При расшифровке - вычитаем по модулю 33
                    //При шифровании - складываем по модулю 33
                    if (cryptDirection) {
                        dectyptedCharPosition = charPosition + keyPosition;
                        if (dectyptedCharPosition >= 33) dectyptedCharPosition -= 33;
                    }
                    else
                    {
                        dectyptedCharPosition = charPosition - keyPosition;
                        if (dectyptedCharPosition < 0) dectyptedCharPosition += 33;
                    }
                    
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

    }
}
