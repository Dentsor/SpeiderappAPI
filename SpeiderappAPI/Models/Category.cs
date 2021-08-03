using System.Collections.Generic;

namespace SpeiderappAPI.Models
{
    public class Category
    {
        public Category(long id, string title)
        {
            Id = id;
            Title = title;

        }

        public long Id { get; init; }
        public string Title { get; set; }
        public List<Tag> Tags { get; set; } = null!;
    }
}
