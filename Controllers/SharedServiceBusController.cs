using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;

namespace jubilant_octo_umbrella.Controllers;

[ApiController]
[Route("[controller]")]
public class SharedServiceBusController : ControllerBase {

    private readonly ILogger<SharedServiceBusController> _logger;
    private readonly ServiceBusClient _serviceBusClient;

    public SharedServiceBusController(ILogger<SharedServiceBusController> logger, ServiceBusClient serviceBusClient) {
        _logger = logger;
        _serviceBusClient = serviceBusClient;
    }

    [HttpGet(Name = "SendToSharedServiceBus")]
    public async Task<IActionResult> Send() {
        _logger.LogInformation("Sending message to SHARED service bus");
        await using var sender = _serviceBusClient.CreateSender("shared");
        await sender.SendMessageAsync(new ServiceBusMessage("Hello, World!"));
        return Ok();
    }
}