namespace DataAccess.Services.Models
{
    public class UpdateUserDto
    {
        public string Auth0Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? CompanyId { get; set; }
    }
}
