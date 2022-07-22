using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace CRMClient {
	public class CRMProvider {
		private readonly IConfiguration _configuration;
        private readonly string BaseUrl;
	    public HttpClient httpClient;
		protected string ApiKey;
		
		public CRMProvider(IConfiguration configuration) {
			_configuration = configuration;
            BaseUrl = _configuration["CRMConfiguration:CrmUrl"];
            httpClient = new HttpClient();
			ApiKey = GetApiKey().Result;
            PrepareHttpClient(ref httpClient);
		}

        private async Task<string> GetApiKey() {
            
            string clientId = _configuration["CRMConfiguration:ClientId"];
            string clientSecret = _configuration["CRMConfiguration:ClientSecret"];
            string tenantId = _configuration["CRMConfiguration:TenantId"];

            string[] scorpes = new string[] { BaseUrl + ".default" };

            var app = ConfidentialClientApplicationBuilder.Create(clientId)
                        .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                        .WithClientSecret(clientSecret)
                        .Build();

            AuthenticationResult? result;
            string token;

            try {
                result = await app.AcquireTokenForClient(scorpes).ExecuteAsync();
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw new Exception("Fatal Error : An Error Occured when trying to retreive the API Token !");
            }

            if (result != null) 
                token = result.AccessToken;
            else
                throw new Exception("Fatal Error : Couldn't Get API TOKEN !");
            return token;
        }

        private void PrepareHttpClient(ref HttpClient client){
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            client.DefaultRequestHeaders.Add("OData-Version", "4.0");
            client.DefaultRequestHeaders.Add("Prefer", "odata.include-annotations = *");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
        }
    }
}
