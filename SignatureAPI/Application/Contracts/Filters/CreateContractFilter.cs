using FluentValidation;
using SignatureAPI.Application.Contracts.Commands;

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
			var result = await _validator.ValidateAsync(contract);

			if (!result.IsValid)
			{
				return Results.BadRequest(new { Errors = result.Errors.Select(x => x.ErrorMessage) });
			}

			return await next(context);
		}
	}
}
