using SignatureAPI.Application.Signatures.Abstractions;
using SignatureAPI.Domain.Enums;

namespace SignatureAPI.Application.Signatures.Services
{
    public class GetSignaturePointsService : IGetSignaturePointsService
	{
		public async Task<SignaturePointsResponse> GetSignatureTotalPoints(SignaturePoints request)
		{
			var total = 0;
			var kingSingned = request.Signature.Contains("K"); 

			foreach(var s in request.Signature)
			{
				switch (s.ToString())
				{
					case nameof(Rol.V) when !kingSingned:
						total = total += (int)Rol.V;
						break;
					case nameof(Rol.N):
						total = total += (int)Rol.N;
						break;
					case nameof(Rol.K):
						total = total += (int)Rol.K;
						break;
				}
			}

			return await Task.FromResult(new SignaturePointsResponse() { Points = total });
		}
	}
}
