namespace Doctorantura.App.Models
{
    public class LineNum
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }

        public int ColumnNumId { get; set; }
        public virtual ColumnNum ColumnNum { get; set; }
    }
}
