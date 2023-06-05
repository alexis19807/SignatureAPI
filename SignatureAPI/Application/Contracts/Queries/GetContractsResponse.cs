using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Contracts.Queries
{
	public class GetContractsResponse
	{
        public IEnumerable<Contract> ContractList { get; set; }
    }
}
