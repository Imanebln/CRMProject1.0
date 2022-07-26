﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService {
	public interface IPrettyEmail : IEmailSender {
		void SendRegister(string to, string link);
		void SendPasswordReset(string to, string link);
	}
}
