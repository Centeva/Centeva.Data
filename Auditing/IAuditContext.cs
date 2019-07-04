using System;
using System.Data.Entity;
using Centeva.Auditing.Models;

namespace Centeva.Data.Auditing {
	public interface IAuditContext:IDisposable {
		DbSet<Audit> Audits { get; set; }
		DbSet<AuditDetail> AuditDetails { get; set; }
		Database Database { get; }


		int SaveChanges();
	}
}
