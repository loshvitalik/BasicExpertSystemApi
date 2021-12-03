using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicExpertSystemApi.Models
{
	public class Error
	{
		public string ErrorName { get; set; }
		public string ErrorDescription { get; set; }

		public Error(string errorName, string errorDescription)
		{
			ErrorName = errorName;
			ErrorDescription = errorDescription;
		}
	}
}