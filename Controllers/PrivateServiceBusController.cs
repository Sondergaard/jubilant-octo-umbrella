using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Azure;

namespace jubilant_octo_umbrella.Controllers;

[ApiController]
[Route("[controller]")]
public class PrivateServiceBusController : ControllerBase 
{
    private readonly ILogger<PrivateServiceBusController> logger;
    private readonly ServiceBusClient serviceBusClient;

    public PrivateServiceBusController(ILogger<PrivateServiceBusController> logger, IAzureClientFactory<ServiceBusClient> factory)
    {
        this.logger = logger;
        this.serviceBusClient = factory.CreateClient("PrivateServiceBus");
    }

    [HttpGet(Name = "SendToPrivateServiceBus")]
    public async Task<IActionResult> Send() {
        logger.LogInformation("Sending message to PRIVATE service bus");
        await using var sender = serviceBusClient.CreateSender("shared");
        await sender.SendMessageAsync(new ServiceBusMessage("Hello, World!"));
        return Ok();
    }
}