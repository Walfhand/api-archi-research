﻿using FluentValidation;

namespace Engine.Validation;
public static class Extensions
{
    public static async Task HandleValidationAsync<TRequest>(this IValidator<TRequest> validator, TRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new Exceptions.ValidationException(validationResult.Errors?.First()?.ErrorMessage ?? "");
        }
    }
}
