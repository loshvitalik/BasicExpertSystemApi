using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BasicExpertSystemApi.Models;
using BasicExpertSystemApi.Services;

namespace BasicExpertSystemApi.Controllers
{
	public class SystemsController : ApiController
	{
		private readonly BESDBContext context = new BESDBContext();

		[Route("api/systems")]
		[HttpGet]
		public IHttpActionResult Get()
		{
			return Ok(context.Systems.Include(s=>s.Rules).ToList());
		}

		[Route("api/systems/{id}")]
		[HttpGet]
		public IHttpActionResult Get(Guid id)
		{
			var system = context.Systems.Include(s => s.Rules).FirstOrDefault(s => s.Id == id);
			if (system == null)
				return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new Error("SystemNotFound", $"System with ID {id} was not found")));
			return Ok(system);
		}

		[Route("api/systems")]
		[HttpPost]
		public IHttpActionResult Post([FromBody] Models.System system)
		{
			if (system.Id == Guid.Empty)
				return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new Error("CheckRequiredParameters", "System ID must not be empty!")));
			context.Systems.Add(system);
			context.SaveChanges();
			return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, system));
		}

		[Route("api/systems/{id}/search")]
		[HttpGet]
		public IHttpActionResult Search(Guid id, string input)
		{
			var system = context.Systems.Include(s => s.Rules).FirstOrDefault(s => s.Id == id);
			if (system == null)
				return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new Error("SystemNotFound", $"System with ID {id} was not found")));
			if (string.IsNullOrEmpty(input))
				return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new Error("CheckRequiredParameters", $"Input text must not be empty!")));
			return Ok(BasicExpertSystem.RunSearch(context, system, input));
		}

		[Route("api/systems")]
		[HttpPatch]
		public IHttpActionResult Patch([FromBody] Models.System newSystem)
		{
			return PatchById(newSystem.Id, newSystem);
		}

		[Route("api/systems/{id}")]
		[HttpPatch]
		public IHttpActionResult PatchById(Guid id, [FromBody] Models.System newSystem)
		{
			var system = context.Systems.Include(s => s.Rules).FirstOrDefault(s => s.Id == id);
			if (system == null)
				return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new Error("SystemNotFound", $"System with ID {id} was not found")));
			system.Name = newSystem.Name;
			foreach (var rule in system.Rules.ToList())
			{
				context.Rules.Remove(rule);
			}

			context.SaveChanges();
			system.Rules = newSystem.Rules;
			context.SaveChanges();
			return Ok(system);
		}

		[Route("api/systems/{id}")]
		[HttpDelete]
		public IHttpActionResult Delete(Guid id)
		{
			var system = context.Systems.FirstOrDefault(s => s.Id == id);
			if (system == null)
				return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new Error("SystemNotFound", $"System with ID {id} was not found")));
			context.Systems.Remove(system);
			context.SaveChanges();
			return Ok();
		}
	}
}
