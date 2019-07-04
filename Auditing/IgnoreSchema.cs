using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centeva.Data.Auditing {
	public class IgnoreSchema:AuditIgnore {
		public string Schema { get; set; }

		public IgnoreSchema(string schema) {
			Schema = schema;
		}
	}
}
