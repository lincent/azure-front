using System.Text.Json;
using Azure.Storage.Blobs;
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
    private readonly BlobServiceClient _blobServiceClient;

    public Cards(ILogger<Cards> logger, BlobServiceClient blobServiceClient)
    {
        _logger = logger;
        _blobServiceClient = blobServiceClient;
    }

    [Function("cards")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req
    )
    {
        var container = _blobServiceClient.GetBlobContainerClient("test");
        var blob = container.GetBlobClient("cards.json");

        if (!blob.Exists()) {
            return new BadRequestObjectResult("Cards could not be found");
        }

        using var memoryStream = new MemoryStream();
        await blob.DownloadToAsync(memoryStream);

        memoryStream.Position = 0;

        var cards = await JsonSerializer.DeserializeAsync<Card[]>(memoryStream);

        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult(cards);
    }
}