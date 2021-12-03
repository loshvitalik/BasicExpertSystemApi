using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace BasicExpertSystemApi.Models
{
	public class ExpertSystemLog
	{
		public Guid Id { get; set; }
		public DateTime Timestamp { get; set; }
		public long ElapsedMilliseconds { get; set; }
		public Guid SystemId { get; set; }
		public string SystemName { get; set; }
		public string InputText { get; set; }
		public string Result { get; set; }
		public string LogCsv { get; set; }

		public ExpertSystemLog(System system, long elapsedMilliseconds, string inputText, string result, string logCsv)
		{
			Id = Guid.NewGuid();
			Timestamp = DateTime.Now;
			ElapsedMilliseconds = elapsedMilliseconds;
			SystemId = system.Id;
			SystemName = system.Name;
			InputText = inputText;
			Result = result;
			LogCsv = logCsv;
		}
	}
}