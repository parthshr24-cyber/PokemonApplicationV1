using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalAPIService.Interfaces
{
    public interface IOpenAIClientService
    {
        Task<string> GenerateOpenAIResponseAsync(string userMessage, string systemMessage);
    }
}
