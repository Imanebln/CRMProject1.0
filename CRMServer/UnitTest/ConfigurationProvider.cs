using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest {
	public class ConfigurationProvider {
		private readonly string _configDirectory = @"C:\Users\Electronic Store\Documents\GitHub\CRMProject1.0\CRMServer\CRMServer\appsettings.json";
		public IConfiguration Configuration { get; set; }
		public ConfigurationProvider() {
			Configuration = new ConfigurationBuilder().AddJsonFile(_configDirectory).Build();
		}
	}
}
