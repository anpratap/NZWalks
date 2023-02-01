namespace NZWalks.API.Repositiories
{
    public interface ITokenHandler
    {
       Task<string> CreateTokenAsync(Models.DTO.User user);
    }
}
