using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Contracts.Services
{
    public class CompareSignaturesResponse
    {
        public Signature? WinnerSignature { get; set; }
    }
}
