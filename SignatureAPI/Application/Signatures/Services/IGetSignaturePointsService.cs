namespace SignatureAPI.Application.Signatures.Services
{
	public interface IGetSignaturePointsService
	{
		Task<SignaturePointsResponse> GetSignatureTotalPoints(SignaturePoints request);
	}
}
