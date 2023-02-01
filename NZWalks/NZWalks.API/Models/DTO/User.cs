namespace NZWalks.API.Models.DTO
{
    public class User
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
