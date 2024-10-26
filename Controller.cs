using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyChatbotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ChatController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] ChatRequest request)
        {
            var requestBody = new
            {
                model = "llama3.2",
                prompt = request.Prompt
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("AI_ENDPOINT_URL", content);  // Replace AI_ENDPOINT_URL with your actual AI service URL

            var responseString = await response.Content.ReadAsStringAsync();
            return Ok(responseString);
        }
    }

    public class ChatRequest
    {
        public string Prompt { get; set; }
    }
}
