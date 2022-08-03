using System.Text.Json.Serialization;

namespace CRMServer.Models.CRM{
	public class Contact : ICrmEntity {
		public Guid ContactId { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? Birthdate { get; set; }
		[JsonPropertyName("emailaddress1")]
		public string Email { get; set; } = String.Empty;
		public string? MobilePhone { get; set; }
		public string? Fax { get; set; }
		public string? JobTitle { get; set; }
		public Account? Account { get; set; }

		[JsonIgnore]
		public DateTime BirthdateObj {
			get {
			    if (Birthdate == null) return DateTime.Now;
				int[] numbers = Birthdate.Split('-').Select(n => int.Parse(n)).ToArray();
				return new DateTime(numbers[0], numbers[1], numbers[2]);
			}
			set {
                Birthdate = value.ToShortDateString();
			}
		}
		public bool IsPrimary {
			get{
			    if (Account == null) return false;
				return ContactId.ToString() == Account.PrimaryContactId;
			}
		}

		public Guid GetId() {
			return ContactId;
		}

		public string GetUnique() {
			return Email;
		}

		public override string? ToString() {
			return $"Contact({ContactId}, {Firstname}, {Lastname}, {Birthdate}, {Email}, {MobilePhone}, {Fax}, {IsPrimary})";
		}
	}
}
