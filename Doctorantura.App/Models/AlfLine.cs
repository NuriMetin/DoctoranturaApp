﻿namespace Doctorantura.App.Models
{
    public class AlfLine
    {
        public int ID { get; set; }
        public int LineNum { get; set; }
        public double Value { get; set; }

        public int CalcTaskId { get; set; }
        public virtual CalcTask CalcTask { get; set; }
    }
}
