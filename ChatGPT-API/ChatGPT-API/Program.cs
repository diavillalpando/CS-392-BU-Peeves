// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using System.Reflection.Metadata.Ecma335;
using OpenAI.Managers;

class Program
{
    static async Task Main(string[] args) // Added async Task
    {
        Console.WriteLine("Please provide the API key: ");
        string api_key = Console.ReadLine();


        var openAIService = new OpenAI.GPT3.Managers.OpenAIService(new OpenAiOptions()
        {
            ApiKey = api_key
        });

        string prompt;

        do
        {
            Console.WriteLine("Give prompt: ");

            prompt = Console.ReadLine();

            var completionResult = await openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser(prompt)
                },
                Model = Models.ChatGpt3_5Turbo,
                MaxTokens = 50
            });

            if (completionResult.Successful && completionResult.Choices.Count > 0) // Check if choices exist
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
            }
            else
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        } while (!string.IsNullOrEmpty(prompt));

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}