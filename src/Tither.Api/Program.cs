using Tither.Domain.Profiles;
using Tither.Ioc;
using Tither.Shared.Settings;
using Microsoft.AspNetCore.OData;
using FluentValidation.AspNetCore;
using Tither.Shared.Validators;
using System.Reflection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<IValidatable>()).
    AddOData(opt =>
    {
        opt.EnableQueryFeatures();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddMvc()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(IValidatable)));
    });

builder.Services.AddSingleton(Log.Logger);

var appSettings = builder.Configuration.GetSection("credentials").Get<AppSettings>();

builder.Services.AddMediatR();

builder.Services.AddAutoMapper(typeof(MemberMappingProfile));
builder.Services.DataContextConfiguration(appSettings);
builder.Services.AddTitherConfiguration(appSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
