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
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(
            new Card[] {
                new() { Id = 1, Content = "Content for card one", Title = "1 Title" },
                new() { Id = 2, Content = "The second card's content", Title = "2 Title" },
                new() { Id = 3, Content = "threeeeeeeeeeeee", Title = "3 Title" }
            });
    }
}