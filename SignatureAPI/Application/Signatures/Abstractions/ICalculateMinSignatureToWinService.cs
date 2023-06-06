using SignatureAPI.Domain.Enums;

namespace SignatureAPI.Application.Signatures.Abstractions
{
    public interface ICalculateMinSignatureToWinService
    {
        Task<Rol> GetMinimunSignatureToWin(Guid id);
    }
}
