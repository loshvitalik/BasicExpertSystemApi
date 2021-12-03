using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasicExpertSystemApi.Models
{
	public class BESDBContext : DbContext
	{
		public DbSet<System> Systems { get; set; }
		public DbSet<Rule> Rules { get; set; }
		public DbSet<ExpertSystemLog> Logs { get; set; }
		public BESDBContext()
			: base()
		{ }
	}
}