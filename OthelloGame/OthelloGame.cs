using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace OthelloGame
{
    public class OthelloGame
    {
        private Board m_Board;
        private int m_CurrentPlayer;
        private ComputerPlayer m_ComputerPlayer;
 
        //Get Set
        public int CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
            set
            {
                m_CurrentPlayer = value;
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
            set
            {
                m_Board = value;
            }
        }

        public ComputerPlayer ComputerPlayer
        {
            get
            {
                return m_ComputerPlayer;
            }
            set
            {
                m_ComputerPlayer = value;
            }
        }

        //Ctor
        public OthelloGame(bool i_IsTwoPlayers, int i_SizeOfBoard)
        {         
           Board = new Board(i_SizeOfBoard);
           if(i_IsTwoPlayers== false)
           {
              ComputerPlayer = new ComputerPlayer(Board.k_O);
           }
        }

        public void ComputerMove()
        {
            ComputerPlayer.MakeMove(ref m_Board);
        }

        public void PlayerMove(int i_ChosenRow, int i_ChosenCol)
        {
            Board.PlacePiece(i_ChosenRow, i_ChosenCol, CurrentPlayer+1);
        }

        public bool IsGameOver(int i_CurrentPlayerColor)
        {
            bool isGameOver = true;
            // Check if the board is full or if there are no more valid moves
            for (int i = 0; i < m_Board.SizeOfBoard; i++)
            {
                for (int j = 0; j < m_Board.SizeOfBoard; j++)
                {
                    if (m_Board.GetPieceAt(i, j) == Board.k_EMPTY && m_Board.IsValidMove(i, j, i_CurrentPlayerColor))
                    {
                        isGameOver = false;
                        break;
                    }

                }

                if (isGameOver == false) { break; }
            }
            return isGameOver;
        }

        public int CountPieces(int i_Color)
        {
            int countPiecesOfTheColor = 0;
            for (int i = 0; i < m_Board.SizeOfBoard; i++)
            {
                for (int j = 0; j < m_Board.SizeOfBoard; j++)
                {
                    if (m_Board.GetPieceAt(i, j) == i_Color)
                    {
                        countPiecesOfTheColor++;
                    }
                }
            }
            return countPiecesOfTheColor;
        }

    }
}
