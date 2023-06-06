using System.Security.Principal;
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

		var response = await CreateContractAsync(client, plaintiffSignature, defendantSignature);

		if (response.IsSuccessStatusCode)
		{
			var result = response.Content.ReadAsStringAsync().Result;
			var contract = JsonConvert.DeserializeObject<Contract>(result);

			if (defendantSignature.FullSignature.Contains('#') || plaintiffSignature.FullSignature.Contains('#'))
			{
				var signature = await CalculateMinSignatureAsync(client, contract.Id);

				Console.WriteLine($"The minumum signature to win is {signature}");
			}
			else
			{
				var signature = await CompareSignaturesAsync(client, contract.Id);

				Console.WriteLine($"The winner is {signature.WinnerSignature.FullSignature}");
			}
		}
		else
		{
			var result = response.Content.ReadAsStringAsync().Result;
			var reponse = JsonConvert.DeserializeObject<Error>(result);

			foreach(var error in reponse.Errors) 
			{
				Console.WriteLine(error);
			}
		}

		Console.WriteLine("Press enter to continue, CTRL+C to exit");
		Console.ReadLine();
	}
}

async static Task<HttpResponseMessage> CreateContractAsync(HttpClient client, Signature plaintiffSignature, Signature defendantSignature)
{
	var json = JsonConvert.SerializeObject(new CreateContract()
	{
		PlaintiffSignature = plaintiffSignature,
		DefendantSignature = defendantSignature
	});

	var data = new StringContent(json, Encoding.UTF8, "application/json");

	return await client.PostAsync("/CreateContract", data);
}

async static Task<CompareSignatureResponse?> CompareSignaturesAsync(HttpClient client, Guid id)
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

async static Task<string?> CalculateMinSignatureAsync(HttpClient client, Guid id)
{
	var response = await client.GetAsync($"/CalculateMinSignature/{id}");

	if (response.IsSuccessStatusCode)
	{
		var result = response.Content.ReadAsStringAsync().Result;
		var signature = JsonConvert.DeserializeObject<Rol>(result);

		return await Task.FromResult(signature.ToString());
	}

	return null;
}

public class CreateContract
{
	public Signature PlaintiffSignature { get; set; }
	public Signature DefendantSignature { get; set; }
}

public class Error
{
    public List<string> Errors { get; set; }
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

public enum Rol
{
	V = 1,
	N = 2,
	K = 5
}