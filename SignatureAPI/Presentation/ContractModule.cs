using Carter;
using MediatR;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Application.Contracts.Filters;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Application.Contracts.Services;

namespace SignatureAPI.Presentation
{
	public class ContractModule : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/CreateContract/", async (CreateContract contract, ISender sender) =>
			{
				var result = await sender.Send(contract);
				return result.Id == null ? Results.BadRequest() : Results.Ok(result);
			}).AddEndpointFilter<CreateContractFilter>();

			app.MapGet("/GetContract/{id}", async (Guid id, ISender sender) =>
			{
				return await sender.Send(new GetContract() { Id = id });
			});

			app.MapGet("/GetContracts/", async (ISender sender) =>
			{
				return await sender.Send(new GetContracts());
			});

			app.MapGet("/CompareSignatures/{id}", async (Guid id, ICompareSignaturesService compareSignaturesService) =>
			{
				return await compareSignaturesService.CompareSignatures(id);
			});
		}
	}
}
