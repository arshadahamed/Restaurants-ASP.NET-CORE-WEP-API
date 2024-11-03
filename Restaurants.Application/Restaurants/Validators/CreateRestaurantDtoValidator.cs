﻿using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "japanese", "American", "Indian"];
    public CreateRestaurantDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        //RuleFor(dto => dto.Description)
        //    .NotEmpty().WithMessage("Description is Required");

        RuleFor(dto => dto.Category)
            .Must(validCategories.Contains)
            .WithMessage("Invalid Category. Please choose from the valid categories.");
            //.Custom((value,context) =>
            //{
            //    var isValidCategory = validCategories.Contains(value);
            //    if(!isValidCategory)
            //    {
            //        context.AddFailure("Category", "Invalid Category. Please choose from the valid categories.");
            //    }
            //});

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress()
            .WithMessage("Please Provide A Valid Email Address");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please Provide A Valid Postal Code (XX-XXX).");
    }
}