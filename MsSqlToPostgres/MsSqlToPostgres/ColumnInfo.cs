﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlToPostgres
{
	public class ColumnInfo
	{
		public string Name { get; set; }
		public string DataType { get; set; }
		public string MaxLength { get; set; }
	}
}
