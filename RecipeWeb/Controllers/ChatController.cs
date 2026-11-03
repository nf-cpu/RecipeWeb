using BLL.Services;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace RecipeWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IAgentService agentService;

        public ChatController(IAgentService agentService)
        {
            this.agentService=agentService;
        }
        [HttpPost]
        public async Task<IActionResult> Ask(ChatRequestDto dto, CancellationToken ct)
        {
            var answer = await agentService.AskAsync(dto.Messages, ct);
            return Ok(new { text = answer });
        }
        [HttpPost("stream")]
        public async Task Stream(ChatRequestDto dto, CancellationToken ct)
        {
            Response.ContentType = "text/event-stream";
            Response.Headers.CacheControl = "no-cache";

            try
            {
                await foreach (var chunk in agentService.AskStreamingAsync(dto.Messages, ct))
                {
                    // JSON כדי ששורה חדשה בתוך הטקסט לא תשבור את הפורמט
                    var json = JsonSerializer.Serialize(new { text = chunk });
                    await Response.WriteAsync($"data: {json}\n\n", ct);
                    await Response.Body.FlushAsync(ct);
                }

                await Response.WriteAsync("data: [DONE]\n\n", ct);
            }
            catch (Exception ex) when (Response.HasStarted && ex is not OperationCanceledException)
            {
                // התשובה כבר התחילה, ולכן ה-Filter כבר לא יכול לשנות אותה
                var json = JsonSerializer.Serialize(new { title = "The answer was interrupted. Please try again." });
                await Response.WriteAsync($"event: error\ndata: {json}\n\n");
            }
        }
    }
}
