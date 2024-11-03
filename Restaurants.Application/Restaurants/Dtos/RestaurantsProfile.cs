using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(d => d.Address, option => option.MapFrom(
                    src => new Address
                    {
                        City = src.City,
                        PostalCode = src.PostalCode,
                        Street = src.Street,
                    }
                    ));
            ;

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(d => d.City, option => 
                option.MapFrom(src => src.Address == null ? null : src.Address.City))
            .ForMember(d => d.PostalCode, option => 
                option.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
            .ForMember(d => d.Street, option => 
                option.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.Dishes, option =>
                option.MapFrom(src => src.Dishes));
    }
}
