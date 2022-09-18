using System.Collections;
using System.Collections.Generic;

namespace Doctorantura.App.Models
{
    public class ColumnNum
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }

        public virtual ICollection<LineNum> LineNums { get; set; }

    }
}
