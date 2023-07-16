﻿using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Common.Behavior;

public class RequestValidationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse> where TRequest:
    IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next
        , CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators.Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        if (failures.Count!=0)
        {
            throw new Exception.ValidationException(failures);
        }

        return await next();

    }
}