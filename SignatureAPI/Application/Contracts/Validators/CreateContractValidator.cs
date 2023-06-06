using FluentValidation;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Domain.Entities;
using System.Text.RegularExpressions;

namespace SignatureAPI.Application.Contracts.Validators
{
	public class CreateContractValidator : AbstractValidator<CreateContract>
	{
		private readonly Regex Regex = new Regex(@"^[KNVknv#]+$");

		public CreateContractValidator()
        {
			RuleFor(x => x.DefendantSignature.FullSignature)
				.NotEmpty()
				.Matches(Regex)
				.MaximumLength(3)
				.WithMessage("Defendant signature has to have a valid value");

			RuleFor(x => x.PlaintiffSignature.FullSignature)
				.NotEmpty()
				.Matches(Regex)
				.MaximumLength(3)
				.WithMessage("Plaintiff signature has to have a valid value");

			RuleFor(x => new { x.DefendantSignature, x.PlaintiffSignature })
				.Must(x => x.DefendantSignature.FullSignature != x.PlaintiffSignature.FullSignature)
				.WithMessage("Plaintiff and Defendant signatures cannot be equals");

			RuleFor(x => new { x.DefendantSignature, x.PlaintiffSignature })
				.Must(x => !(x.DefendantSignature.FullSignature.Count(x => x == '#') > 1 
					|| x.PlaintiffSignature.FullSignature.Count(x => x == '#') > 1))
				.WithMessage("Plaintiff or Defendant signatures cannot have more than one special character each in the signature");
		}
	}
}
