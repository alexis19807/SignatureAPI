using SignatureAPI.Application.Signatures.Responses;

namespace SignatureAPI.Application.Signatures.Abstractions
{
    public interface ICompareSignaturesService
    {
        Task<CompareSignaturesResponse> CompareSignatures(Guid id);
    }
}
