using CRMServer.Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMClient.contracts {
	public interface IAddressService : ICRMBaseService<Address> {
		IEnumerable<Address> GetAllAddresses();
		Address? GetAddressById(Guid guid);
		Task<Address?> UpdateAddress(Address address);
	}
}
