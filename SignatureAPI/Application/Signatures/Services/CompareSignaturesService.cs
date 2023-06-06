using SignatureAPI.Application.Contracts.Abstractions;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Application.Signatures.Abstractions;
using SignatureAPI.Application.Signatures.Requests;
using SignatureAPI.Application.Signatures.Responses;

namespace SignatureAPI.Application.Signatures.Services
{
	public class CompareSignaturesService : ICompareSignaturesService
	{
		private readonly IGetSignaturePointsService _getSignaturePointsService;
		private readonly IContractRepository _contractRepository;

		public CompareSignaturesService(IGetSignaturePointsService getSignaturePointsService,
			IContractRepository contractRepository)
		{
			_getSignaturePointsService = getSignaturePointsService;
			_contractRepository = contractRepository;

		}

		public async Task<CompareSignaturesResponse> CompareSignatures(Guid id)
		{
			var contract = await _contractRepository.GetContract(new GetContract() { Id = id });

			var pointsPlaintiff = await _getSignaturePointsService.GetSignatureTotalPoints(
				new SignaturePoints()
				{
					Signature = contract.SignaturePlaintiff?.FullSignature?.ToUpperInvariant()
				}
			);

			var pointsDefendant = await _getSignaturePointsService.GetSignatureTotalPoints(
				new SignaturePoints()
				{
					Signature = contract.SignatureDefendant?.FullSignature?.ToUpperInvariant()
				}
			);

			if (pointsPlaintiff.Points > pointsDefendant.Points)
			{
				return await Task.FromResult(new CompareSignaturesResponse() { WinnerSignature = contract.SignaturePlaintiff });
			}

			return await Task.FromResult(new CompareSignaturesResponse() { WinnerSignature = contract.SignatureDefendant });
		}
	}
}
