using CodebridgeTestTask.Bll.Extension;
using CodebridgeTestTask.Bll.Interfaces;
using CodebridgeTestTask.Bll.Services;
using CodebridgeTestTask.Dal.Context;
using CodebridgeTestTask.Infrastructure.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new GlobalExceptionFilter()))
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.InvalidModelStateResponseFactory = // the interjection
                            ModelStateValidator.ValidateModelState;
                    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<Context>(options =>
                                                           options.UseSqlServer(builder.Configuration
                                                                                   .GetConnectionString("WebApiDatabase")), ServiceLifetime.Transient);

builder.Services.AddMvc()
                    .AddFluentValidation(config =>
                    {
                        ValidatorOptions.Global.CascadeMode = CascadeMode.Continue;
                        config.RegisterValidatorsFromAssemblyContaining<Program>();
                    })
                    .AddJsonOptions(o =>
                    {
                        o.JsonSerializerOptions.PropertyNamingPolicy = null;
                        o.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    })
                    .AddMvcOptions(o => o.AllowEmptyInputInBodyModelBinding = true);

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueLimit = 0;
    });
    // Changing the status code
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers();

app.Run();
