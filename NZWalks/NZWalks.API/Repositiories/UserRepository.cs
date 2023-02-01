using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositiories
{
    public class UserRepository : IUserRepository
    {
        //private List<User> users = new List<User>()
        //{
        //    new User()
        //    {
        //        FirstName="Read Only",LastName="User",EmailAddress="readonly@user.com",Id=Guid.NewGuid(),UserName="readonly@user.com",
        //        Password="Readonly@user",//Roles=new List<string> { "reader" }
        //    },
        //    new User()
        //    {
        //        FirstName="Read Write",LastName="User",EmailAddress="readowrite@user.com",Id=Guid.NewGuid(),UserName="readwrite@user.com",
        //        Password="Readwrite@user",//Roles=new List<string> { "reader","writer" }
        //    }
        //};
        public UserRepository(NZWalksDbContext nZWalksDb)
        {
            _nZWalksDb = nZWalksDb;
        }

        private readonly NZWalksDbContext _nZWalksDb;

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user= await _nZWalksDb.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);
            var userRoles = await _nZWalksDb.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();

            if (userRoles.Any())
            {
                foreach(var userRole in userRoles)
                {
                    var role=await _nZWalksDb.Roles.FirstOrDefaultAsync(x=>x.Id==userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }
            user.Password = null;
            return user;
        }
    }
}
