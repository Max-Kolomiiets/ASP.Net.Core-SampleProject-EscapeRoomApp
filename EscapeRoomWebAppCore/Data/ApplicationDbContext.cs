using Bogus;
using EscapeRoomWebAppCore.Models;
using EscapeRoomWebAppCore.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EscapeRoomWebAppCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        
        public DbSet<EscapeRoom> EscapeRooms { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            FakeData.Init(10);

            modelBuilder.Entity<EscapeRoom>().HasData(FakeData.Rooms);
        }
    }

    public static class FakeData
    {
        public static List<EscapeRoom> Rooms = new List<EscapeRoom>();

        public static void Init(int count)
        {
            int roomId = 1;
            var roomFaker = new Faker<EscapeRoom>()
               .RuleFor(p => p.Id, _ => roomId++)
               .RuleFor(p => p.Name, f => f.Lorem.Word())
               .RuleFor(p => p.Description, f => f.Lorem.Paragraphs())
               .RuleFor(p => p.TimeLimitMinutes, f => f.Random.Number(20, 100))
               .RuleFor(p => p.MinimumPlayers, f => f.Random.Number(1, 3))
               .RuleFor(p => p.MaximumPlayers, f => f.Random.Number(3, 20))
               .RuleFor(p => p.MinimumPlayerAge, f => f.Random.Number(15, 30))
               .RuleFor(p => p.Address, f => f.Address.City())
               .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
               .RuleFor(p => p.EmailAddress, f => f.Internet.Email())
               .RuleFor(p => p.CompanyName, f => f.Company.CompanyName())
               .RuleFor(p => p.Rating, f => f.Random.Number(1, 10))
               .RuleFor(p => p.FearLevel, f => f.Random.Byte(1, 5))
               .RuleFor(p => p.DifficultyLevel, f => f.Random.Byte(1, 5))
               .RuleFor(p => p.Logotype, f => UrlFileToBytes.GetBytes(f.Image.LoremFlickrUrl()));

            

            //var blogId = 1;
            //var blogFaker = new Faker<Blog>()
            //   .RuleFor(b => b.BlogId, _ => blogId++)
            //   .RuleFor(b => b.Url, f => f.Internet.Url())
            //   .RuleFor(b => b.Posts, (f, b) =>
            //   {
            //       postFaker.RuleFor(p => p.BlogId, _ => b.BlogId);

            //       var posts = postFaker.GenerateBetween(3, 5);

            //       FakeData.Posts.AddRange(posts);

            //       return null; // Blog.Posts is a getter only. The return value has no impact.
            //   });

            var rooms = roomFaker.Generate(count);

            FakeData.Rooms.AddRange(rooms);
        }
    }
}
