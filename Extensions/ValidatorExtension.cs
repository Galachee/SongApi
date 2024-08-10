using System.ComponentModel.DataAnnotations;

namespace SongApi.Extensions;

public static class ValidatorExtension
{
    public static IResult? ValidateAndReturnErrors(this object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model);

        
        bool isValid = Validator.TryValidateObject(model, context, validationResults, true);

        if (!isValid)
        {
            
            var errors = validationResults
                .GroupBy(e => e.MemberNames.FirstOrDefault() ?? string.Empty)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage ?? string.Empty).ToArray()
                );

            
            return Results.BadRequest(errors);
        }

        return null; 
    }
}