using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TrickingLibrary.Data;
using TrickingLibrary.Models;
using TrickingLibrary.Models.Moderation;

namespace TrickingLibrary.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

                if (env.IsDevelopment())
                {
                    ctx.Add(new Difficulty() {Id = "easy", Name = "Easy", Description = "Easy test"});
                    ctx.Add(new Difficulty() {Id = "medium", Name = "Medium", Description = "Medium test"});
                    ctx.Add(new Difficulty() {Id = "hard", Name = "Hard", Description = "Hard test"});
                    ctx.Add(new Category() {Id = "kick", Name = "Kick", Description = "Kick test"});
                    ctx.Add(new Category() {Id = "flip", Name = "Flip", Description = "Flip test"});
                    ctx.Add(new Category() {Id = "transition", Name = "Transition", Description = "Transition test"});
                    ctx.Add(new Trick()
                    {
                        Id = "backwards_roll",
                        Name = "Backwards roll",
                        Description = "test backwards roll",
                        Difficulty = "easy",
                        Categories = new List<TrickCategory>() {new TrickCategory {CategoryId = "flip",}}
                    });
                    ctx.Add(new Trick()
                    {
                        Id = "forwards-roll",
                        Name = "Forwards roll",
                        Description = "Test Forwards roll",
                        Difficulty = "easy",
                        Categories = new List<TrickCategory>() {new TrickCategory {CategoryId = "flip",}}
                    });
                    ctx.Add(new Trick()
                    {
                        Id = "back-flip",
                        Name = "Back flip",
                        Description = "test Back flip",
                        Difficulty = "medium",
                        Categories = new List<TrickCategory>() {new TrickCategory {CategoryId = "flip",}},
                        Prerequisites = new List<TrickRelationship>()
                            {new TrickRelationship {PrerequisiteId = "backwards_roll"}}
                    });
                    ctx.Add(new Submission
                    {
                        TrickId = "back-flip",
                        Description = "At least I've tried.",
                        Video = new Video()
                        {
                            VideoLink = "one.mp4",
                            ThumbLink = "one.jpg"
                        },
                        VideoProcessed = true
                    });
                    ctx.Add(new Submission
                    {
                        TrickId = "back-flip",
                        Description = "At least I've tried. 2",
                        Video = new Video()
                        {
                            VideoLink = "two.mp4",
                            ThumbLink = "two.jpg"
                        },
                        VideoProcessed = true
                    });
                    ctx.Add(new ModerationItem
                    {
                        Target = "forwards-roll",
                        Type = ModerationTypes.Trick
                    });
                    ctx.SaveChanges();
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}