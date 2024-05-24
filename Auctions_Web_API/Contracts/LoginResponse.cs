namespace Auctions_Web_API.Contracts
{
    public record LoginResponse(string token, string[]permissions);
}
