using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Filters.ExceptionFilter.StrategiesExceptions.Interfaces;
using System.Net;

namespace OnlineStore.Filters.ExceptionFilter.StrategiesExceptions;

public class NotFoundExceptionResponse : IExceptionResponse
{
    public JsonResult GetErrorResponse(ExceptionContext exceptionContext, Exception exeption)
    {
        return new JsonResult(exeption.Message)
        {
            StatusCode = (int)HttpStatusCode.NotFound
        };
    }
}