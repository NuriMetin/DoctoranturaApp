namespace Doctorantura.App.Models
{
    public class LineSum
    {
        public int ID { get; set; }
        public int LineNum { get; set; }
        public double TotalSum { get; set; }

        public int CalcTaskId { get; set; }
        public virtual CalcTask CalcTask { get; set; }
    }
}
