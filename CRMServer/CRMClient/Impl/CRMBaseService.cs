using CRMClient.contracts;
using CRMServer.Models.CRM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text;

namespace CRMClient.Impl {
	public abstract class CRMBaseService<T> : ICRMBaseService<T> where T : ICrmEntity{
		protected readonly HttpClient _client;
		public CRMBaseService(CRMProvider context) {
			_client = context.httpClient;
		}

		
		protected IEnumerable<T> GetFromCrm(string query){
			string response = _client.GetAsync(query).Result.Content.ReadAsStringAsync().Result;
			JObject doc = JObject.Parse(response);
			string? entitiesJson = doc.SelectToken("value")?.ToString();
			if (entitiesJson == null) 
				throw new Exception("API Error : No Value Found !");
			List<T> CrmEntities = JsonConvert.DeserializeObject<List<T>>(entitiesJson)??new List<T>();
			Populate(ref CrmEntities, doc);
			return CrmEntities;
		}

		
		protected string GetFilterQuery<S>(string baseUrl, S parameters){
			StringBuilder queryBuilder = new($"{baseUrl}&$filter=");
			PropertyInfo[] properties = typeof(S).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo prop in properties) {
				string? value = prop.GetValue(parameters, null)?.ToString();
				if (value != null)
					queryBuilder.Append($"{prop.Name.ToLower()} eq '{value.ToLower()}' and ");
			}
			string FilteredQuery = queryBuilder.ToString();
			FilteredQuery = FilteredQuery.Remove(FilteredQuery.LastIndexOf("and "));
			return FilteredQuery;
		}

		protected abstract void Populate(ref List<T> entity, JObject doc);

	}
}
