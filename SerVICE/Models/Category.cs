using System.Text.Json.Serialization;

namespace SerVICE.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual ICollection<Service>? Services { get; set; } = new List<Service>();
    }
}
