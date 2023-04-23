using System.Collections;
using System.Collections.Generic;

namespace Doctorantura.App.Models
{
    public class Column
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Row { get; set; }

        public virtual ICollection<ColumnLine> ColumnLines { get; set; }

        public int CalcTaskId { get; set; }
        public virtual CalcTask CalcTask { get; set; }
    }
}
