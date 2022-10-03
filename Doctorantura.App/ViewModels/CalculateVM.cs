using Doctorantura.App.Models;
using System.Collections.Generic;
using System.Linq;

namespace Doctorantura.App.ViewModels
{
    public class CalculateVM
    {
        public List<Column> ColumnNums { get; set; }
        public virtual List<Line> LineNums { get; set; }
        public List<int> GrouppedColumnIds { get; set; }
    }
}
