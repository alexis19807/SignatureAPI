using Carter;
using FluentValidation;
using SignatureAPI.Application.Contracts.Abstractions;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Application.Contracts.Validators;
using SignatureAPI.Application.Signatures.Abstractions;
using SignatureAPI.Application.Signatures.Services;
using SignatureAPI.Domain.Entities;
using SignatureAPI.Persistence.Contracts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<ICompareSignaturesService, CompareSignaturesService>();
builder.Services.AddScoped<IGetSignaturePointsService, GetSignaturePointsService>();
builder.Services.AddScoped<ICalculateMinSignatureToWinService, CalculateMinSignatureToWinService>();
builder.Services.AddSingleton<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IValidator<CreateContract>, CreateContractValidator>();

builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
