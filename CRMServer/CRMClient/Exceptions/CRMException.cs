using System.Text;

namespace CRMClient.Exceptions {
	public class CRMException : Exception {
		public HttpResponseMessage Response { get; set; }
		public string DetailedMessage { get; set; }
		public CRMException(HttpResponseMessage response) {
			Response = response;
			DetailedMessage = response.Content.ReadAsStringAsync().Result;
			
		}

		private string MessageRenderer(){
			StringBuilder sb = new();
			sb.AppendLine($"Http Error Code : {Response.StatusCode}");
			sb.AppendLine($"Http Error Title : {Response.ReasonPhrase}");
			sb.AppendLine($"Http Response :\n{DetailedMessage}");
			return sb.ToString();
		}

	}
}
