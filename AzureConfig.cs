public record AzureConfig
{
    public string SharedServiceBus { get; init; }
    public string PrivateServiceBus { get; init; }

    public static AzureConfig Empty => new();
}
