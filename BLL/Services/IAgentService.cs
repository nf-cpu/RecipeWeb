using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAgentService
    {
        Task<string> AskAsync(List<ChatMessageDto> history, CancellationToken ct);
        IAsyncEnumerable<string> AskStreamingAsync(List<ChatMessageDto> history, CancellationToken ct);
    }
}

