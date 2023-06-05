using MediatR;
using SignatureAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SignatureAPI.Application.Contracts.Commands
{
	public class CreateContract : IRequest<CreateContractResponse>
	{
		[Required]
		public Signature? PlaintiffSignature { get; set; }

		[Required]
		public Signature? DefendantSignature { get; set; }
	}
}
