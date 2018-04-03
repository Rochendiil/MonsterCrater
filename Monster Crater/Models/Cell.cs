using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Monster_Crater.Models
{
    public class Cell
    {
        public int indexX { get;  set; }
        public int indexY { get; set; }
        public CellType type { get; set; }

        public Cell(Point index, CellType type)
        {
            indexX = index.X;
            indexY = index.Y;
            this.type = type;
        }
    }

}