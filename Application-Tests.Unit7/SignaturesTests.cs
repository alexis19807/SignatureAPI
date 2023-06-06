using SignatureAPI.Application.Contracts.Abstractions;
using SignatureAPI.Application.Signatures.Abstractions;
using SignatureAPI.Application.Signatures.Services;
using SignatureAPI.Domain.Entities;
using SignatureAPI.Domain.Enums;
using SignatureAPI.Persistence.Contracts;

namespace Application.Tests.Unit
{
	public class SignaturesTests
	{
		private readonly IGetSignaturePointsService _getSignaturePointsService;
		private readonly ICompareSignaturesService _compareSignaturesService;
		private readonly ICalculateMinSignatureToWinService _calculateMinSignatureToWinService;
		private readonly IContractRepository _contractRepository;

		private readonly List<Rol> Roles = new List<Rol>() { Rol.V, Rol.N, Rol.K };

		public SignaturesTests()
		{
			_getSignaturePointsService = new GetSignaturePointsService();
			_contractRepository = new ContractRepository();
			_compareSignaturesService = new CompareSignaturesService(_getSignaturePointsService, _contractRepository);
			_calculateMinSignatureToWinService = new CalculateMinSignatureToWinService(_getSignaturePointsService, _contractRepository);
		}

		[Fact]
		public async void GetMinimunSignatureToWin_ReturnMinSignature_WhenSignaturesAreValid()
		{
			var contract = new Contract()
			{
				Id = Guid.NewGuid(),
				SignatureDefendant = new Signature() { FullSignature = "N#V" },
				SignaturePlaintiff = new Signature() { FullSignature = "NVV" }
			};

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

			Assert.Equal(Rol.N, signatureToWin);
		}

		[Fact]
		public async void CompareSignatures_ReturnSignature_WhenOneSignatureWin()
		{
			var contract = new Contract()
			{
				Id = Guid.NewGuid(),
				SignatureDefendant = new Signature() { FullSignature = "KN" },
				SignaturePlaintiff = new Signature() { FullSignature = "NVV" }
			};

			var pointsDefendant = await _getSignaturePointsService.GetSignatureTotalPoints(
				new SignaturePoints()
				{
					Signature = contract.SignatureDefendant.FullSignature.ToUpperInvariant()
				}
			);

			Assert.Equal(7, pointsDefendant.Points);
		}
	}
}