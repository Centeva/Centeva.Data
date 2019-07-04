using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centeva.Data.Auditing {
	public class IgnoreTable:AuditIgnore {
		public string Schema { get; set; }
		public string Table { get; set; }

		public IgnoreTable(string schema, string table) {
			Schema = schema;
			Table = table;
		}
	}
}
