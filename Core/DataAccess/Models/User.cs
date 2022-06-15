namespace DataAccess.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Auth0Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IpAddress { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
