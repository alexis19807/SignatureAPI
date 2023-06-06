using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Signatures.Services
{
    public class CompareSignaturesResponse
    {
        public Signature? WinnerSignature { get; set; }
    }
}
