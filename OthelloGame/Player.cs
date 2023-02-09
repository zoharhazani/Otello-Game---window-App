using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OthelloGame
{
    public class Player
    {
        private string m_Name;
        private int m_Color;
        private int m_Score;

        public Player(string i_Name, int i_Color)
        {
            m_Name = i_Name;
            m_Color = i_Color;
            m_Score = 0;
        }

        //get set
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

    }
}
