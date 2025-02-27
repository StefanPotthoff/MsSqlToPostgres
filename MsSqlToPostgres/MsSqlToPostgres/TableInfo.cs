using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlToPostgres
{
	public class TableInfo
	{
		public string Schema { get; set; }
		public string Name { get; set; }
		public List<ColumnInfo> Columns { get; set; }
	}
}
