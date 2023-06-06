using SignatureAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SignatureAPI.Application.Signatures.Requests
{
    public class CompareSignatures
    {
        [Required]
        public Guid Id { get; set; }
    }
}
