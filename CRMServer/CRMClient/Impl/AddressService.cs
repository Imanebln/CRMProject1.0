using CRMClient.contracts;
using CRMServer.Models.CRM;
using NJsonSchema.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMClient.Impl {
	public class AddressService : CRMBaseService<Address>, IAddressService {
		private readonly string BaseQuery = "/api/data/v9.1/customeraddresses";
		public AddressService(CRMProvider context) : base(context) { }

		public IEnumerable<Address> GetAllAddresses() {
			return GetFromCrm(BaseQuery);
		}

		public Address? GetAddressById(Guid guid) {
			string query = $"{BaseQuery}?$filter=customeraddressid eq '{guid}'";
			return GetFromCrm(query).FirstOrDefault();
		}

		public Task<Address?> UpdateAddress(Address address) {
			return Update(address, GetAddressById, BaseQuery);
		}

		protected override PropertyRenameAndIgnoreSerializerContractResolver GetJsonResolver() {
			PropertyRenameAndIgnoreSerializerContractResolver jsonResolver = new();
			Type type = typeof(Address);
			jsonResolver.NamingStrategy = new LowercaseNamingStrategy();
			jsonResolver.IgnoreProperty(type, "addressid");
			jsonResolver.IgnoreProperty(type, "composite");

			return jsonResolver;
		}
	}
}
