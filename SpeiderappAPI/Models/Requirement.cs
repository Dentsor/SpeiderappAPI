using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeiderappAPI.Models
{
    public class Requirement
    {
        public Requirement(string description, DateTime publishTime) : this(description, null!, publishTime)
        {
        }

        public Requirement(string description, User author, DateTime publishTime)
        {
            Description = description;
            Author = author;
            PublishTime = publishTime;
        }

        public long Id { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }
        public User Author { get; set; }
        public DateTime PublishTime { get; init; }
        /*[InverseProperty("Requiree")]*/
        public ICollection<Requirement> Requirers { get; set; } = null!;
        [JsonIgnore]
        public List<RequirementPrerequisite> RequirerPrerequisites { get; set; } = null!;
        /*[InverseProperty("Requirer")]*/
        public ICollection<Requirement> Requirees { get; set; } = null!;
        [JsonIgnore]
        public List<RequirementPrerequisite> RequireePrerequisites { get; set; } = null!;
    }
}
