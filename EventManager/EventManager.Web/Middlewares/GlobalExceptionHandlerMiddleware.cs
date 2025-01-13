using EventManager.Common.Exceptions;

namespace EventManager.Web.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode;
            string message;

            switch (exception)
            {
                case InvalidRequestParametersException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "The parameters in the request were invalid.";
                    break;
                case UserNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = "The user was not found.";
                    break;
                case CredentialsAlreadyExistException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = "User with those credentials exist already.";
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
            }

            var errorResponse = new
            {
                Message = message,
                Details = exception.Message
            };

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }
    }

}
