using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieApp.Application.Exception;

namespace MovieApp.Api.Filter;

public class AppExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case AppException appException:
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
                break;
            }
            case UnauthorizedAccessException unauthorizedAccessException:
            {
                var result = new ObjectResult(new
                {
                    timestamp = DateTime.Now,
                    errorCode = 401,
                    message = unauthorizedAccessException.Message
                })
                {
                    StatusCode = 401
                };
                context.Result = result;
                context.ExceptionHandled = true;
                break;
            }
            default:
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
                break;
            }
        }
    }
}