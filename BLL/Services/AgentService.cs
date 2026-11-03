using DTO;
using Microsoft.Extensions.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AgentService : IAgentService
    {
        private const string SystemPrompt = """
            אתה עוזר מטבח באתר מתכונים.
            ענה בעברית, בקצרה ובנימוס.
            אל תמציא מתכונים!!! חפש אותם בכלים שקיבלת.
            """;

        private readonly IChatClient _chatClient;

        public AgentService(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        private List<ChatMessage> BuildMessages(List<ChatMessageDto> history)
        {
            var messages = new List<ChatMessage>
            {
                new ChatMessage(ChatRole.System, SystemPrompt)
            };

            foreach (var m in history)
            {
                var role = m.Role == "ai" ? ChatRole.Assistant : ChatRole.User;
                messages.Add(new ChatMessage(role, m.Text));
            }

            return messages;
        }

        public async Task<string> AskAsync(List<ChatMessageDto> history, CancellationToken ct)
        {
            var response = await _chatClient.GetResponseAsync(BuildMessages(history), cancellationToken: ct);
            return response.Text;
        }

        public async IAsyncEnumerable<string> AskStreamingAsync(List<ChatMessageDto> history,
    [EnumeratorCancellation] CancellationToken ct)
        {
            var updates = _chatClient.GetStreamingResponseAsync(BuildMessages(history), cancellationToken: ct);

            await foreach (var update in updates)
            {
                // חלק מהעדכונים הם קריאות לכלים, בלי טקסט
                if (!string.IsNullOrEmpty(update.Text))
                    yield return update.Text;
            }
        }
    }
}
