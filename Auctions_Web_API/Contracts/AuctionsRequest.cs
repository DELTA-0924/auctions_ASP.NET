using Auctuons_core.models;
using System.ComponentModel.DataAnnotations;

namespace Auctions_Web_API.Contracts
{
    public record AuctionRequest(
        string Title,
        string Description        ,
        string Finished

        );
    
   
}
