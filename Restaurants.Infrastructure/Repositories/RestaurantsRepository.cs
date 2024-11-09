using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Respositories;
using Restaurants.Infrastructure.Persistence;
using System.ComponentModel;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) 
    : IRestaurantsRepository
{
    public async Task<int> Create(Restaurant restaurant)
    {
        dbContext.Restaurants.Add(restaurant);
        await dbContext.SaveChangesAsync();
        return restaurant.Id;
    }

    public async Task Delete(Restaurant restaurant)
    {
        dbContext.Remove(restaurant);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();
        return restaurants;
    }
    public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPharse)
    {
        var searchPharseLower = searchPharse?.ToLower();

        var restaurants = await dbContext
             .Restaurants
             .Where(r => searchPharseLower == null || (r.Name.ToLower().Contains(searchPharseLower)
                                                    || r.Description.ToLower().Contains(searchPharseLower)))
            .ToListAsync();
        return restaurants;
    }
    
    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefaultAsync(x => x.Id == id);
        return restaurant;
    }

    public  Task Update()
        => dbContext.SaveChangesAsync();
}
