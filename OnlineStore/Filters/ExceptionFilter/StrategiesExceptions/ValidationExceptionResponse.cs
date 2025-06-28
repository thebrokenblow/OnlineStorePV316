using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Filters.ExceptionFilter.StrategiesExceptions.Interfaces;
using System.Net;

namespace OnlineStore.Filters.ExceptionFilter.StrategiesExceptions;

public class ValidationExceptionResponse : IExceptionResponse
{
    private const string Separator = ", ";

    public JsonResult GetErrorResponse(ExceptionContext context, Exception exception)
    {
        var validationException = (ValidationException)exception;

        var errorMessages = validationException.Errors.Select(validationFailure => validationFailure.ErrorMessage);
        var responseMessage = string.Join(Separator, errorMessages);

        return new JsonResult(responseMessage)
        {
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }
}