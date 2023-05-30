using System.Text.Json.Serialization;

namespace SerVICE.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]  
        //TODO: Update collection of services for an user.
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
