using ExternalAPIService.Interfaces;
using Azure;
using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI.Chat;
using System.ClientModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExternalAPIService.Services
{
    public class OpenAIClientService : IOpenAIClientService
    {
        private readonly IConfiguration _config;
        private readonly ChatClient _chatClient;
        private readonly ILogger<OpenAIClientService> _logger;
        public OpenAIClientService(IConfiguration configuration, ChatClient chatClient, ILogger<OpenAIClientService> logger)
        {
            _config = configuration;
            _chatClient = chatClient;
            _logger = logger;
        }

        public async Task<string> GenerateOpenAIResponseAsync(string userMessage, string systemMessage)
        {
            try
            {
                var messages = new List<ChatMessage>
                {
                    ChatMessage.CreateSystemMessage(systemMessage),

                    ChatMessage.CreateUserMessage(userMessage)
                };
                _logger.LogInformation("Starting Chat with Ai Agent");
                ChatCompletion completion =
                    await _chatClient.CompleteChatAsync(messages);
                _logger.LogInformation("Got Chat response from Ai Agent");
                return completion.Content[0].Text;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error while calling OpenAI");
                return string.Empty;
            }
        }
    }
}
