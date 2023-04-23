namespace Doctorantura.App.Models
{
    public class ColumnLine
    {
        public int ID { get; set; }

        public double Value { get; set; }

        public int ColumnId { get; set; }
        public virtual Column Column { get; set; }

        public int LineId { get; set; }
        public virtual Line Line { get; set; }

        public int ColumnNum { get; set; }
        public int LineNum { get; set; }

        public int CalcTaskId { get; set; }
        public virtual CalcTask CalcTask { get; set; }
    }
}
