using CRMClient.contracts;
using CRMClient.Exceptions;
using CRMServer.Models.CRM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NJsonSchema.Infrastructure;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace CRMClient.Impl {
	public abstract class CRMBaseService<T> : ICRMBaseService<T> where T : ICrmEntity{
		protected readonly HttpClient _client;
		protected readonly CRMProvider context;
		public CRMBaseService(CRMProvider context) {
			_client = context.httpClient;
			this.context = context;
		}

		
		protected IEnumerable<T> GetFromCrm(string query){
			HttpResponseMessage httpRequestMessage = _client.GetAsync(query).Result;

			if (!httpRequestMessage.IsSuccessStatusCode){
				if (httpRequestMessage.StatusCode.HasFlag(System.Net.HttpStatusCode.Unauthorized)) {
					context.RefreshToken();
					Console.WriteLine("Retrying : Renewing API.");
					return GetFromCrm(query);
				}else
					throw new CRMException(httpRequestMessage);
			}	
				
			string response = httpRequestMessage.Content.ReadAsStringAsync().Result;
			JObject doc = JObject.Parse(response);
			string? entitiesJson = doc.SelectToken("value")?.ToString();
			if (entitiesJson == null) 
				throw new CRMException(httpRequestMessage);
			List<T> CrmEntities = JsonConvert.DeserializeObject<List<T>>(entitiesJson)??new List<T>();
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

		protected string GetJsonAuto(T entity) {
			return JsonConvert.SerializeObject(entity, new JsonSerializerSettings() {
				NullValueHandling = NullValueHandling.Ignore,
				ContractResolver  = GetJsonResolver()
			}); ;
		}

		private async Task<HttpResponseMessage> ResolveResponse(T CrmEntity, HttpMethod method , string query){
			HttpRequestMessage request = new(method, query);
			if (request.Method != HttpMethod.Delete){
				request.Content = new StringContent(GetJsonAuto(CrmEntity), Encoding.UTF8, "application/json");
			}
			HttpResponseMessage response = await _client.SendAsync(request);
			
			if (!response.IsSuccessStatusCode)
				throw new CRMException(response);
			return response;
		}

		protected abstract PropertyRenameAndIgnoreSerializerContractResolver GetJsonResolver();
	}

	public class LowercaseNamingStrategy : NamingStrategy {
		protected override string ResolvePropertyName(string name) {
			return name.ToLower();
		}
	}
}
