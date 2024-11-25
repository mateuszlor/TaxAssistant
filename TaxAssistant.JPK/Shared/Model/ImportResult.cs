using TaxAssistant.JPK.Shared.Model.Database;

namespace TaxAssistant.JPK.Shared.Model
{
	[Serializable]
	public class ImportResult
	{
		public bool IsSuccessful { get; set; }
		public Error? Error { get; set; }
		public Import Data { get; set; }
	}
}
