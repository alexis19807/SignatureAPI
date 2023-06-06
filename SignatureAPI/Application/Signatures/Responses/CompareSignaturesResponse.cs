using SignatureAPI.Domain.Entities;

namespace SignatureAPI.Application.Signatures.Responses
{
    public class CompareSignaturesResponse
    {
        public Signature? WinnerSignature { get; set; }
    }
}
