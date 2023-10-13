using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stock.UI.Code.Rest
{
	public abstract class BaseRestClient
	{
		private static string BASE_URL = "https://localhost:7177/api";
		private HttpClient client;

		public BaseRestClient()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri(BASE_URL);
		}

		protected async Task<T> SendRequestAsync<T>(string resource, HttpMethod method, object requestBody = null)
		{
			using (var request = new HttpRequestMessage(method, resource))
			{
				if (requestBody != null)
				{
					string json = JsonSerializer.Serialize(requestBody);
					request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
				}

				using (var response = await client.SendAsync(request))
				{
					if (response.IsSuccessStatusCode)
					{
						var content = await response.Content.ReadAsStringAsync();
						return JsonSerializer.Deserialize<T>(content);
					}
					else
					{
						// Handle error here
						throw new HttpRequestException($"HTTP Request failed with status code: {response.StatusCode}");
					}
				}
			}
		}
	}
}
