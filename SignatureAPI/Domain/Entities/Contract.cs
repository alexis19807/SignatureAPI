namespace SignatureAPI.Domain.Entities
{
	public class Contract
	{
		public Guid Id { get; set; }
		public Signature? SignaturePlaintiff { get; set; }
		public Signature? SignatureDefendant { get; set; }
	}
}
