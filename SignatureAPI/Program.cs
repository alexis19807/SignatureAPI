using Carter;
using FluentValidation;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Application.Contracts.Services;
using SignatureAPI.Application.Contracts.Validators;
using SignatureAPI.Application.Signatures.Services;
using SignatureAPI.Domain.Entities;
using SignatureAPI.Persistence.Contracts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<ICompareSignaturesService, CompareSignaturesService>();
builder.Services.AddTransient<IGetSignaturePointsService, GetSignaturePointsService>();
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
