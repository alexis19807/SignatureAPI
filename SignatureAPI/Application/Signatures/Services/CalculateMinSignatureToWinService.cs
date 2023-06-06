using SignatureAPI.Application.Contracts.Abstractions;
using SignatureAPI.Application.Contracts.Queries;
using SignatureAPI.Application.Signatures.Abstractions;
using SignatureAPI.Domain.Enums;

namespace SignatureAPI.Application.Signatures.Services
{
    public class CalculateMinSignatureToWinService : ICalculateMinSignatureToWinService
	{
		private readonly IGetSignaturePointsService _getSignaturePointsService;
		private readonly IContractRepository _contractRepository;

		private readonly List<Rol> Roles = new List<Rol>() { Rol.V, Rol.N, Rol.K };

	public CalculateMinSignatureToWinService(IGetSignaturePointsService getSignaturePointsService,
			IContractRepository contractRepository)
		{
			_getSignaturePointsService = getSignaturePointsService;
			_contractRepository = contractRepository;
		}

		public async Task<Rol> GetMinimunSignatureToWin(Guid id)
		{

			var contract = await _contractRepository.GetContract(new GetContract() { Id = id });

			var pointsPlaintiff = await _getSignaturePointsService.GetSignatureTotalPoints(
				new SignaturePoints()
				{
					Signature = contract.SignaturePlaintiff.FullSignature.ToUpperInvariant()
				}
			);

			var pointsDefendant = await _getSignaturePointsService.GetSignatureTotalPoints(
				new SignaturePoints()
				{
					Signature = contract.SignatureDefendant.FullSignature.ToUpperInvariant()
				}
			);

			var total = Math.Abs(pointsPlaintiff.Points - pointsDefendant.Points);

			var signatureToWin = Roles.Where(x => (int)x > total).First();

			return signatureToWin;
		}
	}
}
