
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Dtos;

public class CreateRestaurantDto
{
    [StringLength(100, MinimumLength =3)]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Insert a Valid Category")]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }

    [EmailAddress(ErrorMessage = "Please Provide A Valid Email Address")]
    public string? ContactEmail { get; set; }

    [Phone(ErrorMessage = "Please Provide A Valid Phone Number")] 
    public string? ContactNumber { get; set; }


    public string? City { get; set; }
    public string? Street { get; set; }

    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please Provide A Valid Postal Code (XX-XXX).")]
    public string? PostalCode { get; set; }

}
