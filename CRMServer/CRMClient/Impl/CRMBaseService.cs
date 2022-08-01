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

		public async Task<T?> Insert(T CrmEntity, Func<string, T?> FindUnique, string BaseQuery) {
			T? entity = FindUnique(CrmEntity.GetUnique());
			if ( entity != null)
				return default;

			string query = BaseQuery.Split('?')[0];
			_ = await ResolveResponse(CrmEntity, HttpMethod.Post, query);

			return FindUnique(CrmEntity.GetUnique());
		}

		public async Task<T?> Update(T CrmEntity, Func<Guid, T?> FindById, string BaseQuery) {
			if (FindById(CrmEntity.GetId()) == null)
				return default;

			string query = BaseQuery.Split('?')[0] + $"({CrmEntity.GetId()})";
			_ = await ResolveResponse(CrmEntity, HttpMethod.Patch, query);

			return FindById(CrmEntity.GetId());
		}

		protected async Task<T?> Delete(T CrmEntity, Func<Guid, T?> FindById, string BaseQuery) {
			T? entity = FindById(CrmEntity.GetId());
			if (entity == null) 
				return default;

			string query = BaseQuery.Split('?')[0] + $"({CrmEntity.GetId()})";

			_ = await ResolveResponse(CrmEntity, HttpMethod.Delete, query);

			return entity;
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

		protected string GetJson(T entity) {
			StringBuilder sb = new("{");
			PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach(PropertyInfo prop in properties) {
				Object? value = prop.GetValue(entity, null);
				Type? t = value?.GetType();
				if (value != null && t == typeof(string) && !prop.Name.StartsWith("_")) {
					sb.Append($"\"{prop.Name.ToLower()}\": \"{value}\",");
				}
			}
			string objectJson = sb.ToString().TrimEnd(',');
			return objectJson + "}";
		}

		private async Task<HttpResponseMessage> ResolveResponse(T CrmEntity, HttpMethod method , string query){
			HttpRequestMessage request = new(method, query);

			if (request.Method != HttpMethod.Delete){
				request.Content = new StringContent(GetJson(CrmEntity), Encoding.UTF8, "application/json");
			}
				
			HttpResponseMessage response = await _client.SendAsync(request);
			if(!response.IsSuccessStatusCode)
				throw new Exception($"CRM ERROR :\n{response.Content.ReadAsByteArrayAsync().Result}");
			return response;
		}

		protected abstract void Populate(ref List<T> entity, JObject doc);

	}
}
