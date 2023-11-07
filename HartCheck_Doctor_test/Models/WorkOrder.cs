using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HartCheck_Doctor_test.Models
{
    public class WorkOrder
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [JsonPropertyName("resource")]
        public int? ResourceId { get; set; }

        [JsonIgnore]
        public Resource? Resource { get; set; }

        public int Ordinal { get; set; }
        public DateTime OrdinalPriority { get; set; }
        public string? Color { get; set; }
    }

    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [JsonIgnore]
        public Group Group { get; set; }

        [JsonIgnore]
        public int GroupId { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("children")]
        public ICollection<Resource> Resources { get; set; }
    }
}