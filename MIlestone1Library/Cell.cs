using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIlestone1Library
{
    //Cell model class
    public class Cell
    {
        //properties define an individual cell
        public int Id { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public Boolean visited { get; set; } = false;
        public Boolean live { get; set; } = false;
        public bool flagged { get; set; }
        public int liveNeighbors { get; set; } = 0;

        public Cell(int r, int c)
        {
            row = r;
            col = c;
        }
    }
}
