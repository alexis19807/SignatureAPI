using System.Text;
using Newtonsoft.Json;

using (var client = new HttpClient())
{
	client.BaseAddress = new Uri("http://localhost:5234");

	while (true)
	{
		Console.WriteLine("Set Plaintiff Signature");
		var plaintiffSignature = new Signature() { FullSignature = Console.ReadLine() };

		Console.WriteLine("Set Defendant Signature");
		var defendantSignature = new Signature() { FullSignature = Console.ReadLine() };

		var response = await CreateContract(client, plaintiffSignature, defendantSignature);

		if (response.IsSuccessStatusCode)
		{
			var result = response.Content.ReadAsStringAsync().Result;
			var contract = JsonConvert.DeserializeObject<Contract>(result);

			var signature = await CompareSignatures(client, contract.Id);

			Console.WriteLine($"The winner is {signature.WinnerSignature.FullSignature}");
		}
		else
		{
			Console.WriteLine(response.ReasonPhrase);
		}

		Console.WriteLine("Press enter to continue, CTRL+C to exit");
		Console.ReadLine();
	}
}

async static Task<HttpResponseMessage> CreateContract(HttpClient client, Signature plaintiffSignature, Signature defendantSignature)
{
	var json = JsonConvert.SerializeObject(new CreateContract()
	{
		PlaintiffSignature = plaintiffSignature,
		DefendantSignature = defendantSignature
	});

	var data = new StringContent(json, Encoding.UTF8, "application/json");

	return await client.PostAsync("/CreateContract", data);
}

async static Task<CompareSignatureResponse?> CompareSignatures(HttpClient client, Guid id)
{
	var response = await client.GetAsync($"/CompareSignatures/{id}");

	if (response.IsSuccessStatusCode)
	{
		var result = response.Content.ReadAsStringAsync().Result;
		var signature = JsonConvert.DeserializeObject<CompareSignatureResponse>(result);

		return await Task.FromResult(signature);
	}

	return null;
}

public class CreateContract
{
	public Signature PlaintiffSignature { get; set; }
	public Signature DefendantSignature { get; set; }
}

public class Signature
{
	public string FullSignature { get; set; }
}

public class CompareSignatureResponse
{
	public Signature WinnerSignature { get; set; }
}

public class Contract
{
	public Guid Id { get; set; }
}