using SignatureAPI.Application.Signatures.Services;

namespace SignatureAPI.Application.Signatures.Abstractions
{
    public interface IGetSignaturePointsService
    {
        Task<SignaturePointsResponse> GetSignatureTotalPoints(SignaturePoints request);
    }
}
