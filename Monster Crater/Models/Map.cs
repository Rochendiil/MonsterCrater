using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Monster_Crater.Models
{
    public class Map
    {

        public int MapSizeW { get; set; }
        public int MapSizeH { get; set; }
        public int CellSizeW { get; set; }
        public int CellSizeH { get; set; }
        public int CellCountW { get; set; }
        public int CellCountH { get; set; }
        public List<Cell> Cells { get; set; }
        public Map(Size mapSize, Size cellSize, Size cellCount)
        {
            Cells = new List<Cell>();
            MapSizeW = mapSize.Width;
            MapSizeH = mapSize.Height;
            CellSizeW = cellSize.Width;
            CellSizeH = cellSize.Height;
            CellCountW = cellCount.Width;
            CellCountH = cellCount.Height;

           
           for (int y = 0; y < CellCountH; y++)
                {
                for (int i = 0; i < CellCountW; i++)
                {
                    Cell cel;
                    if (y == 0 || y == CellCountH -1)
                    {
                        Point index = new Point(i, y);
                        cel = new Cell(index, CellType.Wall);
                    }
                    else if (i == 0 || i == CellCountW - 1)
                    {
                        Point index = new Point(i, y);
                        cel = new Cell(index, CellType.Wall);
                    }
                    else
                    {
                        Point index = new Point(i, y);
                        cel = new Cell(index, CellType.Normal);
                    }
                    Cells.Add(cel);
                }
            }
        }
    }
}