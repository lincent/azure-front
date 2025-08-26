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
public class Cards
{
    private readonly ILogger<Cards> _logger;

    public Cards(ILogger<Cards> logger)
    {
        _logger = logger;
    }

    [Function("cards")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req,
        [BlobInput("test/cards.json", Connection = "BlobConnectionString")] Card[] cards
    )
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(cards);
    }
}