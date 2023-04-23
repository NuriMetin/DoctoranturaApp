using System;
using System.Collections;
using System.Collections.Generic;

namespace Doctorantura.App.Models
{
    public class CalcTask
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<AlfLine> AlfLines { get; set; }
        public virtual ICollection<Column> Columns { get; set; }
        public virtual ICollection<ColumnLine> ColumnLines { get; set; }
        public virtual ICollection<XLine> XLines { get; set; }
        public virtual ICollection<QamLine> QamLines { get; set; }
        public virtual ICollection<Line> Lines { get; set; }
        public virtual ICollection<LineSum> LineSums { get; set; }
        public virtual ICollection<WLine> WLines { get; set; }



    }
}