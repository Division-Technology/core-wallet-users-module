// <copyright file="ExceptionHandlingMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Users.Application.Exceptions;

namespace Users.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;
    private readonly IWebHostEnvironment env;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
    {
        this.next = next;
        this.logger = logger;
        this.env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Unhandled exception caught for {Path}", context.Request.Path);
            await this.HandleExceptionAsync(context, ex, context.TraceIdentifier);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, string traceId)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var response = new ErrorResponse
        {
            TraceId = traceId,
        };

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                response.Message = "Validation Failed";
                response.Errors = validationEx.Errors
                    .Select(x => new ValidationError { PropertyName = x.PropertyName, ErrorMessage = x.ErrorMessage })
                    .ToList();
                break;

            case NotFoundException notFoundEx:
                statusCode = HttpStatusCode.NotFound;
                response.Message = notFoundEx.Message;
                break;

            case BadRequestException badRequestEx:
                statusCode = HttpStatusCode.BadRequest;
                response.Message = badRequestEx.Message;
                break;

            default:
                response.Message = this.env.IsDevelopment()
                    ? exception.Message
                    : "An internal server error occurred.";
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

public class ErrorResponse
{
    public int StatusCode => (int)this.Status;

    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;

    public string Message { get; set; } = string.Empty;

    public string TraceId { get; set; } = string.Empty;

    public List<ValidationError>? Errors { get; set; }
}

public class ValidationError
{
    public string PropertyName { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;
}
