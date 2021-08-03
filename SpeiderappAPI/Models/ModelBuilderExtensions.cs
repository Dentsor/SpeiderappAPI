using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SpeiderappAPI.Models
{
    public static class ModelBuilderExtensions
    {
        // Inserts test data
        public static void Seed(this ModelBuilder modelBuilder)
        {
            User ozzie = new(-1, "ocrispe0@jugem.jp", "Ozzie", "Crispe");
            modelBuilder.Entity<User>().HasData(
                ozzie,
                new User(-2, "adandye@hexun.com", "Aggi", "Dandy"),
                new User(-3, "gspottiswood2@psu.com", "Gerry", "Spottiswood")
            );

            Badge woodchuck = new("", "", "", DateTime.Now)
            {
                Id = -1L,
                Title = "Woodchuck",
                Image = "3aas!2d=",
                Description = "This is a cool badge for chucking wood.",
                PublishTime = DateTime.Now,
                AuthorId = ozzie.Id
            };
            modelBuilder.Entity<Badge>().HasData(woodchuck);

            Requirement chopWood = new("", DateTime.Now)
            {
                Id = -2L,
                Description = "Actually chop wood",
                PublishTime = DateTime.Now,
                AuthorId = ozzie.Id
            };
            modelBuilder.Entity<Requirement>().HasData(chopWood);

            var prerequisite = new { RequirerId = woodchuck.Id, RequireeId = chopWood.Id, IsAdvisory = false };
            modelBuilder.Entity<RequirementPrerequisite>().HasData(prerequisite);
        }
    }
}
