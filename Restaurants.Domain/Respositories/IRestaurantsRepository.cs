using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Respositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPharse, int pageSize, int pageNumber);
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task Update();
}
