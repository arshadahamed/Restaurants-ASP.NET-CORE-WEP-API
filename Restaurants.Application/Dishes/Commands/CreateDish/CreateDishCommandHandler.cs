using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Contants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Respositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger, 
    IMapper mapper, 
    IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantAuthorizationService,
    IDishesRepository dishesRepository)
    : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish : {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            throw new ForbidException();

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.Create(dish);


    }
}
