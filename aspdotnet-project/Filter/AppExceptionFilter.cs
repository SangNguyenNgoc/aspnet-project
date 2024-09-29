using course_register.API.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace course_register.API.Filter;

public class AppExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AppException appException)
        {
            var result = new ObjectResult(new
            {
                timestamp = DateTime.Now,
                errorCode = appException.ErrorCode,
                message = appException.Message,
                messages = appException.Messages
            })
            {
                StatusCode = appException.ErrorCode
            };
            context.Result = result;
            context.ExceptionHandled = true;
        }
        else
        {
            var result = new ObjectResult(new
            {
                timestamp = DateTime.Now,
                errorCode = 500,
                message = context.Exception.Message
            })
            {
                StatusCode = 500
            };
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}