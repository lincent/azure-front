using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace api;

public record Card
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}


public class FirstFunction
{
    private readonly ILogger<FirstFunction> _logger;
    private readonly Card[] _cards;

    public FirstFunction(ILogger<FirstFunction> logger)
    {
        _logger = logger;
        _cards = [
            new Card{Id = 1, Title = "One", Content = "First content item"},
            new Card{Id = 2, Title = "Two", Content = "Second content item"},
            new Card{Id = 3, Title = "Three", Content = "Third content item"},
            new Card{Id = 4, Title = "Four", Content = "Forth content item"}
        ];
    }

    [Function("cards")]

    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req,
        [BlobInput("test/cards.json", Connection = "BlobConnectionString")] Card[] cards)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var random = new Random().Next(1, cards.Length + 1);
        _logger.LogInformation("Returning cards between 1 and {random}", random);
        return new OkObjectResult(cards[..random]);
    }
}