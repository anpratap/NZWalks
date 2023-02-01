using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public interface IUserRepository
    {
      Task<User>  AuthenticateAsync(string username, string password);

    }
}
