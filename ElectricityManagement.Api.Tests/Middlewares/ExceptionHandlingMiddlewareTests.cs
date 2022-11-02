using ElectricityManagement.Api.Middlewares;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using Xunit;

namespace ElectricityManagement.Api.Tests.Middlewares;

public class ExceptionHandlingMiddlewareTests
{
    private readonly DefaultHttpContext httpContext;
    private readonly Mock<ILogger<ExceptionHandlingMiddleware>> iloggerMock;
    private ExceptionHandlingMiddleware? _sut;
    private RequestDelegate? next;

    public ExceptionHandlingMiddlewareTests()
    {
        httpContext = new();
        iloggerMock = new();
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeCorrectly_WhenValidationException()
    {
        // Arrange
        var exceptionMessage = "Validation Failed";
        var exception = new ValidationException(exceptionMessage);
        next = (httpContext) =>
        {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionHandlingMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_WhenNullReferenceException()
    {
        // Arrange
        var exception = new NullReferenceException();
        next = (httpContext) =>
        {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionHandlingMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_WhenOperationCanceledException()
    {
        // Arrange
        var exception = new OperationCanceledException();
        next = (httpContext) =>
        {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionHandlingMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.ServiceUnavailable, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }

    [Fact]
    public async void HandleExceptionAsync_SetsStatusCodeAndErrorMessageCorrectly_WhenDefaultException()
    {
        // Arrange
        var exception = new Exception();
        next = (httpContext) =>
        {
            return Task.FromException(exception);
        };
        var contentType = "application/json";
        _sut = new ExceptionHandlingMiddleware(next, iloggerMock.Object);

        // Act
        await _sut.InvokeAsync(httpContext);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
        Assert.Equal(contentType, httpContext.Response.ContentType);
    }
}
