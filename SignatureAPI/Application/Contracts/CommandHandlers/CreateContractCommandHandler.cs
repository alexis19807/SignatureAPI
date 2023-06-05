using MediatR;
using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Domain.Entities;
using SignatureAPI.Persistence.Contracts;

namespace SignatureAPI.Application.Contracts.CommandHandlers
{
    public class CreateContractCommandHandler : IRequestHandler<CreateContract, CreateContractResponse>
    {
        private readonly IContractRepository _contractRepository;
        
        public CreateContractCommandHandler(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        public async Task<CreateContractResponse> Handle(CreateContract request, CancellationToken cancellationToken)
        {
            var result = await _contractRepository.CreateContract(request);

            return await Task.FromResult(result);
        }
    }
}
