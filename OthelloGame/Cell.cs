using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OthelloGame
{
    public class Cell
    {
        private int m_Col;
        private int m_Row;

        public Cell()
        {

        }

        public Cell(int i_row, int i_col)
        {
            Row = i_row;
            Col = i_col;
        }

        // get set
        public int Col
        {
            get { return m_Col; }
            set { m_Col = value; }
        }

        public int Row
        {
            get { return m_Row; }
            set { m_Row = value; }
        }
    }
}
