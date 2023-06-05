using FluentValidation;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Contracts.Filters
{
	public class CreateContractFilter : IEndpointFilter
	{
		private readonly IValidator<CreateContract> _validator;
        public CreateContractFilter(IValidator<CreateContract> validator)
        {
			_validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
		{
			var contract = context.Arguments.FirstOrDefault(a => a.GetType() == typeof(CreateContract)) as CreateContract;
			var results = await _validator.ValidateAsync(contract!);
			
			if (!results.IsValid) 
			{
				return Results.Json(results.Errors, statusCode: 400);
			}

			return await next(context);
		}
	}
}
