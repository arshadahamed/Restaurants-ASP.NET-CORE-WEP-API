using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Contants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System.ComponentModel;

namespace Restaurants.Infrastructure.Seeders;

internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                dbContext.Restaurants.AddRange(restaurants);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
             {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User),
                new (UserRoles.Owner),
                new (UserRoles.Admin),
            ];

        return roles;
    }

    private IEnumerable<Restaurant> GetRestaurants()
    {
        List<Restaurant> restaurants = [
            new ()
            {
                Name = "KFC",
                Category = "Fast Food",
                Description = "KFC (Short for Kentucky Friend Chicken) is a restaurant.",
                ContactEmail = "contact@kfc.com",
                HasDelivery = true,
                Dishes =
                [
                    new ()
                    {
                        Name = "Hot Chicken",
                        Description = "Nashville Hot Chicken",
                        Price = 10.30M,
                    },
                    new ()
                    {
                        Name = "Chicken Nuggests",
                        Description = "Nashville Chicken Nuggests",
                        Price = 5.30M,
                    },
                ],
                Address = new ()
                {
                    City = "London",
                    Street = "Cork St 5",
                    PostalCode = "WC2N 5DU"
                }
            },
            new ()
            {
                Name = "McDonald",
                Category = "Fast Food",
                Description = "McDonald's Corporation is a restaurant.",
                ContactEmail = "contact@mcdonald.com",
                HasDelivery = true,
                Address = new ()
                {
                    City = "London",
                    Street = "boots 12B",
                    PostalCode = "W1F 8SR"
                }

            }
        ];
        return restaurants;
    }
}
