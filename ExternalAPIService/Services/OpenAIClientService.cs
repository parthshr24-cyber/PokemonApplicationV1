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

namespace ExternalAPIService.Services
{
    public class OpenAIClientService : IOpenAIClientService
    {
        private readonly IConfiguration _config;
        private readonly ChatClient _chatClient;
        public OpenAIClientService(IConfiguration configuration, ChatClient chatClient)
        {
            _config = configuration;
            _chatClient = chatClient;
        }

        public async Task<string> GenerateOpenAIResponseAsync(string userMessage, string systemMessage)
        {
            var messages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage(systemMessage),

                ChatMessage.CreateUserMessage(userMessage)
            };
            ChatCompletion completion =
                await _chatClient.CompleteChatAsync(messages);

            return completion.Content[0].Text;
        }
    }
}
