using FluentValidation;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ElectricityManagement.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong: {Ex}", ex);
                await HandleExceptionAsync(httpcontext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            StringBuilder errorMessage = new();

            if (exception is ValidationException validationException)
            {
                foreach (var error in validationException.Errors)
                {
                    errorMessage = ExceptionMessageSetter(error.ErrorMessage);
                }

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (exception is NullReferenceException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                errorMessage = ExceptionMessageSetter(exception.Message);
            }
            else if (exception is BadHttpRequestException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorMessage = ExceptionMessageSetter(exception.Message);
            }

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = errorMessage.ToString(),
            }.JsonSerialize());
        }

        private static StringBuilder ExceptionMessageSetter(string exceptionMessage)
        {
            StringBuilder errorMessage = new();
            errorMessage.Append(exceptionMessage);
            errorMessage.AppendLine();

            return errorMessage;
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public string JsonSerialize()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
