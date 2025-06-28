using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Filters.ExceptionFilter.StrategiesExceptions;
using OnlineStore.Filters.ExceptionFilter.StrategiesExceptions.Interfaces;
using OnlineStore.Storage.Exceptions;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace OnlineStore.Filters.ExceptionFilter
{
    public class ErrorExceptionFilter : IExceptionFilter
    {
        private readonly Dictionary<Type, IExceptionResponse> _exceptionHandlers;
        private readonly JsonSerializerOptions _jsonOptions;

        public ErrorExceptionFilter()
        {
            _exceptionHandlers = new Dictionary<Type, IExceptionResponse>
            {
                [typeof(ValidationException)] = new ValidationExceptionResponse(),
                [typeof(NotFoundException)] = new NotFoundExceptionResponse()
            };

            _jsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();

            if (_exceptionHandlers.TryGetValue(exceptionType, out var handler))
            {
                context.Result = handler.GetErrorResponse(context, context.Exception);
            }
            else
            {
                context.Result = new JsonResult(
                    new { Message = "Ошибка системы. Повторите попытку позже." },
                    _jsonOptions)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}