using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Centeva.Data {
	public abstract class DbContextWithMiddleware : DbContext {
		private readonly List<DbContextMiddleware> _middleware;
		private bool _disposed;

		protected DbContextWithMiddleware(params DbContextMiddleware[] middleware) {
			_middleware = middleware.ToList();
			((IObjectContextAdapter) this).ObjectContext.ObjectMaterialized += ObjectContextOnObjectMaterialized;
		}

		public DbContextWithMiddleware(string nameOrConnectionString, params DbContextMiddleware[] middleware) : base(nameOrConnectionString) {
			_middleware = middleware.ToList();
			((IObjectContextAdapter) this).ObjectContext.ObjectMaterialized += ObjectContextOnObjectMaterialized;
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			_middleware.ForEach(m => m.BeforeModelCreating(this, modelBuilder));
			base.OnModelCreating(modelBuilder);
			_middleware.ForEach(m => m.AfterModelCreating(this, modelBuilder));
		}

		private void ObjectContextOnObjectMaterialized(object sender, ObjectMaterializedEventArgs e) {
			_middleware.ForEach(m => m.OnObjectMaterialized(this, e));
		}

		public override int SaveChanges() {
			_middleware.ForEach(m => m.BeforeSaveChanges(this));
			var result = base.SaveChanges();
			_middleware.ForEach(m => m.AfterSaveChanges(this));
			return result;
		}

		public override Task<int> SaveChangesAsync()
		{
			return SaveChangesAsync(CancellationToken.None);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken) {
			_middleware.ForEach(m => m.BeforeSaveChanges(this));
			var result = await base.SaveChangesAsync(cancellationToken);
			_middleware.ForEach(m => m.AfterSaveChanges(this));
			return result;
		}

		protected override void Dispose(bool disposing) {
			if (!_disposed) {
				((IObjectContextAdapter) this).ObjectContext.ObjectMaterialized -= ObjectContextOnObjectMaterialized;
				foreach (var m in _middleware.OfType<IDisposable>()) {
					m.Dispose();
				}

				base.Dispose(disposing);
			}

			_disposed = true;
		}

		public T GetMiddleWare<T>() where T : DbContextMiddleware {
			return _middleware.OfType<T>().FirstOrDefault();
		}

		public IReadOnlyList<DbContextMiddleware> Middleware => _middleware.AsReadOnly();
	}
}
