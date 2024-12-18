﻿using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Contants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Respositories;
using Restaurants.Infrastructure.Persistence;
using System.ComponentModel;
using System.Linq.Expressions;

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
    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPharse,
        int pageSize, 
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPharseLower = searchPharse?.ToLower();

        var baseQuery = dbContext
             .Restaurants
             .Where(r => searchPharseLower == null || (r.Name.ToLower().Contains(searchPharseLower)
                                                    || r.Description.ToLower().Contains(searchPharseLower)));

        var totalCount = await baseQuery.CountAsync();

        if(sortBy != null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name),r => r.Name },
                { nameof(Restaurant.Description),r => r.Description },
                { nameof(Restaurant.Category),r => r.Category },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = await baseQuery
             .Skip(pageSize * (pageNumber - 1))
             .Take(pageSize)
             .ToListAsync();
        return (restaurants,totalCount);
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
