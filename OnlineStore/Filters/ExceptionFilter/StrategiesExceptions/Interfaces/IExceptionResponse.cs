using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineStore.Filters.ExceptionFilter.StrategiesExceptions.Interfaces;

public interface IExceptionResponse
{
    JsonResult GetErrorResponse(ExceptionContext exceptionContext, Exception exeption);
}