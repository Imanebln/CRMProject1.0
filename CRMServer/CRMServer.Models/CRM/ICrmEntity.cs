﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServer.Models.CRM {
	public interface ICrmEntity {
		Guid GetId();
		string GetUnique();
	}
}
