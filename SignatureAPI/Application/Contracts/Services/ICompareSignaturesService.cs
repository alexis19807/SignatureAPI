namespace SignatureAPI.Application.Contracts.Services
{
    public interface ICompareSignaturesService
    {
        Task<CompareSignaturesResponse> CompareSignatures(Guid id);
    }
}
