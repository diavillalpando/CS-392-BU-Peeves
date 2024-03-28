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
    static async Task Main(string[] args)
    {
        // prompting the user for the API key
        Console.WriteLine("Please provide the API key: ");
        string api_key = Console.ReadLine();

        // Initializing the OpenAI service with the provided API key
        var openAIService = new OpenAI.GPT3.Managers.OpenAIService(new OpenAiOptions()
        {
            ApiKey = api_key
        });

        string prompt;
        
        // a while loop that continuously receives prompts from the user unless they enter an empty string
        do
        {
            Console.WriteLine("Give prompt: (Hit enter to exit.)");

            prompt = Console.ReadLine();

            // if the user entered a string that is null or empty, end the chat
            if (string.IsNullOrEmpty(prompt))
            {
                break;
            }
            
            // generating completion for the user
            var completionResult = await openAIService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."), // giving a system message to provide the model with a generic context
                    ChatMessage.FromUser(prompt) // adding the user prompt
                },
                Model = Models.ChatGpt3_5Turbo,
                MaxTokens = 50
            });

            // if the competion was successful, send the output
            if (completionResult.Successful && completionResult.Choices.Count > 0) 
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
            }
            else // if not, send the error message
            {
                if (completionResult.Error == null)
                {
                    throw new Exception("Unknown Error");
                }
                Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
            }
        } while (!string.IsNullOrEmpty(prompt)); // if the prompt from the user was null or empty, exit

    }
}