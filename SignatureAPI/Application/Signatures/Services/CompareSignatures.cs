﻿using SignatureAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SignatureAPI.Application.Signatures.Services
{
    public class CompareSignatures
    {
        [Required]
        public Guid Id { get; set; }
    }
}
