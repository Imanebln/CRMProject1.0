﻿using Newtonsoft.Json;

namespace CRMServer.Models.CRM{
	public class Contact : ICrmEntity {
		public Guid ContactId { get; set; }
		public string? Firstname { get; set; }
		public string? Lastname { get; set; }
		public string? Birthdate { get; set; }
		[JsonProperty(PropertyName = "emailaddress1")]
		public string Email { get; set; } = String.Empty;
		public string? MobilePhone { get; set; }
		public string? Fax { get; set; }
		public string? JobTitle { get; set; }
		[JsonProperty(PropertyName = "entityimage")]
		public string? ImageUrl { get; set; }
		[JsonProperty(PropertyName = "parentcustomerid_account")]
		public Account? Account { get; set; }
		[JsonProperty(PropertyName = "Contact_CustomerAddress")]
		public List<Address>? Addresses { get; set; } = new List<Address>();

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
		private bool primary = false;
		public bool IsPrimary {
			get {
			    if (Account == null) return primary;
				return ContactId.ToString() == Account.PrimaryContactId;
			} set {
				primary = value;
			}
		}

		public Guid GetId() {
			return ContactId;
		}

		public string GetUnique() {
			return Email;
		}

		public override string? ToString() {
			return $"Contact({ContactId}, {Firstname}, {Lastname}, {Birthdate}, {Email}, {MobilePhone}, {Fax}, {IsPrimary}, \n\tAccount({Account})\n)";
		}
		public override bool Equals(object? obj) {
			if (obj == null) return false;
			if (obj.GetType()==typeof(Contact)){
				Contact contact = (Contact)obj;
				return contact.ContactId == ContactId;
			}else{
				return false;
			}
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}
