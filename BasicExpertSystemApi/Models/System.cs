using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace BasicExpertSystemApi.Models
{
	public class System
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<Rule> Rules { get; set; }
	}
}