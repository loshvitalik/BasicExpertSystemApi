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
				sb.Append("Pass,Position,Input,Result\n");
				var query = inputText;
				sb.Append("0,0," + query + "\n");
				var result = "";
				var rules = system.Rules.OrderBy(r => r.Position).ToList();
				var skippedRules = new List<int>();
				var finished = false;
				var passNumber = 1;

				while (!finished)
				{
					finished = true;
					var conditions = query.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).Select(c=>c.Trim()).ToList();
					for (var i = 0; i < rules.Count; i++)
					{
						if (skippedRules.Contains(i)) continue;
						var rule = rules[i];
						var premises = rule.Condition.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
						var intersection = conditions.Intersect(premises);
						var isPremiseCorrect = premises.All(p => intersection.Contains(p));

						if (!isPremiseCorrect) continue;
						skippedRules.Add(i);
						finished = false;
						sb.Append(passNumber + "," + rule.Position + "," + query + "," + rule.Result + "\n");
						query += "; " + rule.Result;
						conditions = query.Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).Select(c => c.Trim()).ToList();
						result = rule.Result;
					}

					passNumber++;
				}

				var log = sb.ToString();
				sw.Stop();
				var logEntity = new ExpertSystemLog(system, sw.ElapsedMilliseconds, inputText, result, log);
				context.Logs.Add(logEntity);
				context.SaveChanges();
				return new Result(system.Id, result.Length != 0, result, log);
			}
			catch (Exception e)
			{
				return new Result(system.Id, false, e.Message);
			}
		}
	}
}