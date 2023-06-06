using SignatureAPI.Application.Contracts.Abstractions;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Persistence.Contracts
{
    public class ContractRepository : IContractRepository
	{
		public List<Contract> Contracts = new List<Contract>();

		public async Task<IEnumerable<Contract>> GetAllContracts()
		{
			return await Task.FromResult(Contracts);
		}

		public async Task<Contract> GetContract(GetContract request)
		{
			return await Task.FromResult(Contracts.FirstOrDefault(c => c.Id == request.Id) ?? new Contract());
		}

		public async Task<CreateContractResponse> CreateContract(CreateContract request)
		{
			var id = Guid.NewGuid();

			var contract = new Contract()
			{
				Id = id,
				SignaturePlaintiff = request.PlaintiffSignature,
				SignatureDefendant = request.DefendantSignature
			};

			Contracts.Add(contract);

			return await Task.FromResult(new CreateContractResponse() { Id = id });
		}
	}
}
