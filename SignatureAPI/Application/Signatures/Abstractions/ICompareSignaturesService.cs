using SignatureAPI.Application.Signatures.Services;

namespace SignatureAPI.Application.Signatures.Abstractions
{
    public interface ICompareSignaturesService
    {
        Task<CompareSignaturesResponse> CompareSignatures(Guid id);
    }
}
