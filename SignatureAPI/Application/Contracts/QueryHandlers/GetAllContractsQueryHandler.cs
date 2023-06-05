using MediatR;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Persistence.Contracts;

namespace SignatureAPI.Application.Contracts.QueryHandlers
{
	public class GetAllContractsQueryHandler : IRequestHandler<GetContracts, GetContractsResponse>
	{
		private readonly IContractRepository _contractRepository;

		public GetAllContractsQueryHandler(IContractRepository contractRepository)
        {
			_contractRepository = contractRepository;
		}

        public async Task<GetContractsResponse> Handle(GetContracts request, CancellationToken cancellationToken)
		{
			var result = await _contractRepository.GetAllContracts();
			return await Task.FromResult(new GetContractsResponse() { ContractList = result });
		}
	}
}
