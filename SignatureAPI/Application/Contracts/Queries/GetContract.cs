using MediatR;
using System.ComponentModel.DataAnnotations;

namespace SignatureAPI.Application.Contracts.Queries
{
	public class GetContract : IRequest<GetContractResponse>
	{
		[Required]
		public Guid? Id { get; set; }
	}
}
