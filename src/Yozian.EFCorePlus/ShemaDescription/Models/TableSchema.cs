using System;
using System.Collections.Generic;
using System.Text;

namespace Yozian.EFCorePlus.ShemaDescription.Models
{
    public class TableSchema
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ColumnSchema> Columns { get; set; }
    }
}
