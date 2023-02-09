using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OthelloGameUI
{
    public partial class OthelloGameSetting : Form
    {
        private const int k_MinBoardSize = 6;
        private const int k_MaxBoardSize = 12;
        private int m_BoardSize;
        private bool m_IsTwoPlayers = false;

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }

        public bool IsTwoPlayers
        {
            get
            {
                return m_IsTwoPlayers;
            }
            set
            {
                m_IsTwoPlayers = value;
            }
        }

        public OthelloGameSetting()
        {
            InitializeComponent();
        }

        private void OthelloGameSetting_Load(object sender, EventArgs e)
        {
            BoardSize = 6;
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            string buttonText;

            if (BoardSize == k_MaxBoardSize)
            {
                BoardSize = k_MinBoardSize;
            }
            else
            {
                BoardSize = BoardSize + 2;
            }
            
            buttonText = string.Format("Board Size {0}x{1} (Click to increase)", m_BoardSize, m_BoardSize);
            (sender as Button).Text = buttonText;
        }

        private void buttonPlayAgainstTheComputer_Click(object sender, EventArgs e)
        {
            IsTwoPlayers = false;
            this.Visible = false;
            this.Close();
        }

        private void buttonPlayAgainstYourFriend_Click(object sender, EventArgs e)
        {
            IsTwoPlayers = true;
            this.Visible = false;
            this.Close();
        }
    }
}
