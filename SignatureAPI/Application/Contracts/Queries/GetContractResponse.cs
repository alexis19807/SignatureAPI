using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Contracts.Queries
{
	public class GetContractResponse
	{
        public Contract? Contract { get; set; }
    }
}
