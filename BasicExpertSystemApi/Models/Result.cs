using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace BasicExpertSystemApi.Models
{
	public class Result
	{
		public Guid SystemId { get; set; }
		public bool Success { get; set; }
		public string ResultText { get; set; }
		public string Log { get; set; }

		public Result(Guid systemId, bool success, string resultText, string log = "")
		{
			SystemId = systemId;
			Success = success;
			ResultText = resultText;
			Log = log;
		}
	}
}