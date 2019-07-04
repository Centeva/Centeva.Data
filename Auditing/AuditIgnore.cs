using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centeva.Data.Auditing {
	public abstract class AuditIgnore {
		public static AuditIgnore Create(string schema) {
			return new IgnoreSchema(schema);
		}

		public static AuditIgnore Create(string schema, string table) {
			return new IgnoreTable(schema, table);
		}

		public static AuditIgnore Create(string schema, string table, string column) {
			return new IgnoreColumn(schema, table, column);
		}
	}
}
