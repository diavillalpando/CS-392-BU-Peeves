// See https://aka.ms/new-console-template for more information
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;

var gpt3 = new OpenAIService(new OpenAiOptions()
{
    ApiKey = ""
});


Console.WriteLine("Enter Prompt:");
string prompt = Console.ReadLine();
var completionResult = await gpt3.Completions.CreateCompletion(new CompletionCreateRequest()
{
    Prompt = prompt,
    Model = Models.ChatGpt3_5Turbo,
    Temperature = 0.5F,
    MaxTokens = 100
});

if (completionResult.Successful)
{
    foreach(var choice in completionResult.Choices)
    {
        Console.WriteLine(choice.Text);
    }
}
else
{
    if (completionResult.Error == null)
    {
        throw new Exception("Unknown Error");
    }
    Console.WriteLine($"{completionResult.Error.Code}: {completionResult.Error.Message}");
}


