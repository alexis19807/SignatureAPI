using MediatR;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Domain.Entities;
using SignatureAPI.Persistence.Contracts;

namespace SignatureAPI.Application.Contracts.QueryHandlers
{
    public class GetContractQueryHandler : IRequestHandler<GetContract, GetContractResponse>
    {
		private readonly IContractRepository _contractRepository;

		public GetContractQueryHandler(IContractRepository contractRepository)
		{
			_contractRepository = contractRepository;
		}

		public async Task<GetContractResponse> Handle(GetContract request, CancellationToken cancellationToken)
        {
			var result = await _contractRepository.GetContract(request);
			return await Task.FromResult(new GetContractResponse() { Contract = result});
        }
    }
}
