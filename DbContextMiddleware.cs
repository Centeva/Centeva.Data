using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Centeva.Data {
	public abstract class DbContextMiddleware {
		public virtual void BeforeModelCreating(DbContext context, DbModelBuilder modelBuilder) { }
		public virtual void AfterModelCreating(DbContext context, DbModelBuilder modelBuilder) { }
		public virtual void OnObjectMaterialized(DbContext context, ObjectMaterializedEventArgs e) { }
		public virtual void BeforeSaveChanges(DbContext context) { }
		public virtual void AfterSaveChanges(DbContext context) { }
	}
}
