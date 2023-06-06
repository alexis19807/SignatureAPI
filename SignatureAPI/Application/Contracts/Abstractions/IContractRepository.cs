using SignatureAPI.Application.Contracts.Commands;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Contracts.Abstractions
{
    public interface IContractRepository
    {
        Task<IEnumerable<Contract>> GetAllContracts();
        Task<Contract> GetContract(GetContract request);
        Task<CreateContractResponse> CreateContract(CreateContract request);
    }
}
