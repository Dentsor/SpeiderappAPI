using System;
using System.Collections.Generic;

namespace SpeiderappAPI.Models
{
    public class Badge : Requirement
    {
        public Badge(string title, string image, string description, DateTime publishTime) : base(description, publishTime)
        {
            Title = title;
            Image = image;
            Resources = Array.Empty<string>();
        }
        // public Badge(string title, string image, string description, User author,
        //     DateTime publishTime) : base(description,
        //     author, publishTime)
        // {
        //     Title = title;
        //     Image = image;
        //     Resources = Array.Empty<string>();
        // }

        public string Title { get; set; }
        public string Image { get; set; }
        public ICollection<TaggedWith> TaggedWiths { get; set; } = null!;
        public string[] Resources { get; set; }
    }
}
