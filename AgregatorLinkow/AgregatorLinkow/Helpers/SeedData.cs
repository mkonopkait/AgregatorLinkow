using AgregatorLinkow.Data;
using AgregatorLinkow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorLinkow.Helpers
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                var users = new List<User>()
                {
                    new User() {
                        Id = Guid.NewGuid().ToString(),
                        Email = "mk1@mk.pl",
                        UserName = "mk1@mk.pl",
                        PasswordHash = "AQAAAAEAACcQAAAAEB6Tctwfd2GBjfvLPTDdtYDMisfNDJpy81+clUgduZ+GKshv8Vc0jwufZ+zIzhqfXA=="
                    },
                    new User(){
                        Id = Guid.NewGuid().ToString(),
                        Email = "mk2@mk.pl",
                        UserName = "mk2@mk.pl",
                        PasswordHash = "AQAAAAEAACcQAAAAEB6Tctwfd2GBjfvLPTDdtYDMisfNDJpy81+clUgduZ+GKshv8Vc0jwufZ+zIzhqfXA=="
                    }
                };
                context.Users.AddRange(users);

                if (context.Links.Any())
                {
                    return;
                }

                var links = new List<Link>();

                for(int i = 0; i<6*24; i++)
                {
                    links.Add(new Link
                    {
                        Date = DateTime.UtcNow.AddHours(-i),
                        PlusesNumber = i,
                        Title = $"Link {i}",
                        Url = $"www.{i}.pl",
                        UserId = i % 2 == 0 ? users.ElementAt(0).Id : users.ElementAt(1).Id
                    });
                }

                context.Links.AddRange(links);
                context.SaveChanges();
            }
        }
    }
}
