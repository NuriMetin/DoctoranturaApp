﻿using Doctorantura.App.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Doctorantura.App.ViewModels
{
    public class CalculateVM
    {
        public List<ColumnLine> ColumnLines { get; set; }
        public List<LineSum> LineSums { get; set; }
        public List<WLine> WLines { get; set; }
        public List<AlfLine> AlfLines { get; set; }
        public List<QamLine> QamLines { get; set; }
        public List<XLine> XLines { get; set; }
        public CalcTask CalcTask { get; set; }
        public int TaskId { get; set; }
    }
}
