using SignatureAPI.Application.Signatures.Requests;
using SignatureAPI.Application.Signatures.Responses;

namespace SignatureAPI.Application.Signatures.Abstractions
{
    public interface IGetSignaturePointsService
    {
        Task<SignaturePointsResponse> GetSignatureTotalPoints(SignaturePoints request);
    }
}
