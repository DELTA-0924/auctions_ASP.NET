namespace Auctions_Web_API.Contracts
{
    public record UserProfileResponse(string Username,string Email
                                        ,string Firstname,string Surname,
                                        int? age,string Aboutme,string Company);
}
