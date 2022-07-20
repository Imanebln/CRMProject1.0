using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRMClient
{
    public class ContactService
    {
        private HttpClient client;
        private HttpRequestHeaders headers;
        private string APIKey = string.Empty;

        public ContactService()
        {
            client = new HttpClient();
            headers = this.client.DefaultRequestHeaders;
            APIKey = GetApiKey().Result;
            Console.WriteLine(APIKey);
            //headers.Add("Authorization", string.Concat("Bearer ", APIKey));

        }

        public async Task<string> GetApiKey()
        {
            string clientId = "cf3594d3-b53c-4f9e-a65d-6f0fbce3863c";
            string clientSecret = "S-G8Q~-WHeD2eLpD5SiYVpSBDP0LZGzZKO0f5b91";
            string authority = "https://login.microsoftonline.com/4e55b7c0-2a72-478c-924d-eb1fefbd1136";
            string resourceUrl = "https://org8d5a8085.crm4.dynamics.com";

            ClientCredential credentials = new ClientCredential(clientId, clientSecret);
            var authContext = new AuthenticationContext(authority);
            var result = await authContext.AcquireTokenAsync(resourceUrl, credentials);
            return result.AccessToken;

        }

        public async Task<HttpResponseMessage> GetContacts()
        {
            string url = "https://org8d5a8085.crm4.dynamics.com/api/data/v9.2/contacts";
            var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Get, url);
            msg.Headers.Add("OData-MaxVersion", "4.0");
            msg.Headers.Add("OData-Version", "4.0");
            msg.Headers.Add("Prefer", "odata.include-annotations=\"*\"");

            msg.Headers.Add("Authorization", $"Bearer {APIKey}");

            return await client.SendAsync(msg);
        }
    }
}
