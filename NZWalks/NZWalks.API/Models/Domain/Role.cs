namespace NZWalks.API.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserRoles> UserRoles { get; set; }
    }
}
