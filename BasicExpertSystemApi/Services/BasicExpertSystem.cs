using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using BasicExpertSystemApi.Models;

namespace BasicExpertSystemApi.Services
{
	public class BasicExpertSystem
	{
		public static Result RunSearch(BESDBContext context, Models.System system, string inputText)
		{
			try
			{
				var sw = new Stopwatch();
				sw.Start();
				var sb = new StringBuilder();
				sb.Append("Position;Input;Result\n");
				var query = inputText;
				sb.Append(query + "\n");
				var result = "";
				var rules = system.Rules.OrderBy(r => r.Position).ToList();
				var skippedRules = new List<int>();
				var finished = false;

				while (!finished)
				{
					finished = true;
					var conditions = query.Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
					for (var i = 0; i < rules.Count; i++)
					{
						if (skippedRules.Contains(i)) continue;
						var rule = rules[i];
						var premises = rule.Condition.Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
						var intersection = conditions.Intersect(premises);
						var isPremiseCorrect = premises.All(p => intersection.Contains(p));

						if (!isPremiseCorrect) continue;
						skippedRules.Add(i);
						finished = false;
						sb.Append(rule.Position + ";" + query.Replace(";", ",") + ";" + rule.Result + "\n");
						query += "; " + rule.Result;
						conditions = query.Split(new[] {"; "}, StringSplitOptions.RemoveEmptyEntries);
						result = rule.Result;
					}
				}

				var log = sb.ToString();
				sw.Stop();
				var logEntity = new ExpertSystemLog(system, sw.ElapsedMilliseconds, inputText, result, log);
				context.Logs.Add(logEntity);
				context.SaveChanges();
				return new Result(system.Id, true, result, log);
			}
			catch (Exception e)
			{
				return new Result(system.Id, false, e.Message);
			}
		}
	}
}