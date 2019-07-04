using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centeva.Data.Auditing {
	public class IgnoreColumn:AuditIgnore {
		public string Schema { get; set; }
		public string Table { get; set; }
		public string Column { get; set; }

		public IgnoreColumn(string schema, string table, string column) {
			Schema = schema;
			Table = table;
			Column = column;
		}
	}
}
