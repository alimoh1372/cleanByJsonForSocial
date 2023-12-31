﻿using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ValidationException:System.Exception
{
    public ValidationException()
    :base("One or more validation failures have occurred.")
    {
        Failures = new Dictionary<string, string[]>();
    }

    public IDictionary<string, string[]> Failures;

    public ValidationException(List<ValidationFailure> failures)
        : this()

    {
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();
        foreach (string propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();
            Failures.Add(propertyName,propertyFailures);
        }
    }
}