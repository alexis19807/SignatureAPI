using FluentValidation;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Domain.Entities;
using System.Text.RegularExpressions;

namespace SignatureAPI.Application.Contracts.Validators
{
	public class CreateContractValidator : AbstractValidator<CreateContract>
	{
        public CreateContractValidator()
        {
			RuleFor(x => x.DefendantSignature)
				.NotEmpty();

			RuleFor(x => x.DefendantSignature.FullSignature)
				.Matches(new Regex(@"^[KNVknv]+$"));

			RuleFor(x => x.PlaintiffSignature)
				.NotEmpty();

			RuleFor(x => x.PlaintiffSignature.FullSignature)
				.Matches(new Regex(@"^[KNVknv]+$"));

			RuleFor(x => new { x.DefendantSignature, x.PlaintiffSignature })
				.Must(x => x.DefendantSignature.FullSignature != x.PlaintiffSignature.FullSignature);
		}
    }
}
