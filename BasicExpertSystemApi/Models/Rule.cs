using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BasicExpertSystemApi.Models
{
	public class Rule
	{
		public Guid Id { get; set; }
		public int Position { get; set; }
		public string Condition { get; set; }
		public string Result { get; set; }
	}
}