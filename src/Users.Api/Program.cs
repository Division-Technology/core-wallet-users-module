// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using Users.Api.Grpc;
using Users.Api.Middlewares;
using Users.Application;
using Users.Application.Handlers.Users.Commands;
using Users.Application.Handlers.Users.Queries;
using Users.Application.Validators.Users;
using Users.Data;
using Users.Domain.Entities.Users.Commands.Create;
using Users.Domain.Entities.Users.Commands.PatchUpdate;
using Users.Domain.Entities.Users.Queries.GetById;
using Users.Grpc;
using Users.Installment.Common;
using Users.Installment.Domains;

var builder = WebApplication.CreateBuilder(args);

// --- Services ---
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddSingleton<IValidator<CreateUserCommandRequest>, CreateUserCommandRequestValidator>();
builder.Services.AddSingleton<IValidator<PatchUpdateUserCommand>, PatchUpdateUserCommandValidator>();

builder.Services.AddScoped<Users.Repositories.Users.IUsersRepository, Users.Repositories.Users.UsersRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddHealthChecks();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(AssemblyReference).Assembly));

// Register Users and UserClients Installments
builder.InstallUsers();

// Register AutoMapper profiles for both Users and UserClients
builder.Services.AddAutoMapper(
    typeof(Users.Application.Mappings.Users.UserProfile).Assembly);

// builder.InstallCommon();
// builder.InstallUsers();

// --- App ---
var app = builder.Build();

// --- Apply DB Migrations automatically in Dev/Local ---
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(5));

    retryPolicy.Execute(() =>
    {
        dbContext.Database.Migrate();
    });
}

// --- Middleware ---
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseGrpcWeb();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseHttpsRedirection();

// --- Map Endpoints ---
app.MapGrpcService<UsersGrpcService>().EnableGrpcWeb();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
