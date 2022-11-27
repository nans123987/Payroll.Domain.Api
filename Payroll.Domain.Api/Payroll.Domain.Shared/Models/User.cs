using System.Text.Json.Serialization;

namespace Payroll.Domain.Shared.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public Roles Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
