using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OthelloGame;
using System.Timers;

namespace OthelloGameUI
{
    public partial class OthelloGameForm : Form
    {
        private int m_SizeOfBoard;
        private bool m_IsTwoPlayers;
        private OthelloGame.OthelloGame m_Game;       
        private PictureBoxCell[,] m_BoardPicBox;
        private Player[] m_PlayersArray;
        private List<int> m_WinnerPerRound;

        //Get Set 
        public int SizeOfBoard
        {
            get
            {
                return m_SizeOfBoard;
            }
            set
            {
                m_SizeOfBoard = value;
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

        public OthelloGame.OthelloGame Game
        {
            get
            {
                return m_Game;
            }
            set
            {
                m_Game = value;
            }
        }

        public Player[] PlayersArray
        {
            get
            {
                return m_PlayersArray;
            }
            set
            {
                m_PlayersArray = value;
            }
        }

        public PictureBoxCell[,] BoardPicBox
        {
            get
            {
                return m_BoardPicBox;
            }
            set
            {
                m_BoardPicBox = value;
            }
        }

        public List<int> WinnerPerRound
        {
            get
            {
                return m_WinnerPerRound;
            }
            set
            {
                m_WinnerPerRound = value;
            }
        }

        //Ctor
        public OthelloGameForm(bool i_IsTwoPlayers, int i_BoardSize)
        {
            InitializeComponent();           
            WinnerPerRound = new List<int>();
            SizeOfBoard = i_BoardSize;
            IsTwoPlayers = i_IsTwoPlayers;
            PlayersArray = new Player[2];
            initializePlayersArray();           
            BoardPicBox = new PictureBoxCell[SizeOfBoard, SizeOfBoard];
            Game = new OthelloGame.OthelloGame(IsTwoPlayers, SizeOfBoard);
            this.Padding = new Padding(15, 15, 15, 15);
        }

        private void initializePlayersArray()
        {
            PlayersArray[0] = new Player("Red", Board.k_X);
            if (IsTwoPlayers == false)
            {
                PlayersArray[1] = new Player("Computer", Board.k_O);
            }
            else { PlayersArray[1] = new Player("Yellow", Board.k_O); }

        }

        public void CreatePicBoxBoard()
        {
            int buttonSize = 50;
            int margin = 5;
            int offset = 15;

            for (int row = 0; row < SizeOfBoard; row++)
            {
                for (int col = 0; col < SizeOfBoard; col++)
                {
                    PictureBoxCell picBoxButton = new PictureBoxCell(row, col);
                    picBoxButton.Size = new Size(buttonSize, buttonSize);               
                    picBoxButton.Location = new Point(offset + (col * buttonSize) + (col * margin), offset + (row * buttonSize) + (row * margin));
                    picBoxButton.Image = Properties.Resources.CoinYellow;
                    picBoxButton.Click += Button_Click;
                    BoardPicBox[row, col] = picBoxButton;
                    this.Controls.Add(picBoxButton);
                }
            }
        }

        public void UpdatePicBoxBoard(int[,] i_Matrix)
        {
            for (int row = 0; row < SizeOfBoard; row++)
            {
                for (int col = 0; col < SizeOfBoard; col++)
                {
                    switch (i_Matrix[row, col])
                    {
                        case 1:
                            BoardPicBox[row, col].Image = Properties.Resources.CoinRed;  
                            BoardPicBox[row, col].Enabled = false;
                            break;
                        case 2:
                            BoardPicBox[row, col].Image = Properties.Resources.CoinYellow;
                            BoardPicBox[row, col].Enabled = false;
                            break;
                        case 3:
                            BoardPicBox[row, col].Image = Properties.Resources.GreenPicture;
                            BoardPicBox[row, col].Enabled = true;                          
                            break;
                        case 0:
                            BoardPicBox[row, col].Image = Properties.Resources.GrayPicture;
                            BoardPicBox[row, col].Enabled = false;
                            break;
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {            
            PictureBoxCell button = (PictureBoxCell)sender;
            MakeMoveAfterClick(button.Row, button.Col);
        }

        public void MakeMoveAfterClick(int i_Row, int i_Col)
        {
            Game.Board.DeleteGreenCellsAfterMove();
            Game.PlayerMove(i_Row, i_Col);
            UpdatePicBoxBoard(Game.Board.Matrix);
            changeTurn();           
            startRound();           
        }

        private void othelloGameForm_Load(object sender, EventArgs e)
        {
            CreatePicBoxBoard();            
            Game.CurrentPlayer = getRandomChoiceBet0and1();
            startRound();
        }

        private int getRandomChoiceBet0and1()
        {
            Random rand = new Random();
            return rand.Next(2);
        }

        private void computersTurn()
        {
            Game.ComputerMove();           
            changeTurn();
            startRound();
        }

        private void startRound()
        {
            this.Text = string.Format("Othello - {0}'s Turn ", PlayersArray[Game.CurrentPlayer].Name);

            if (Game.IsGameOver(PlayersArray[Game.CurrentPlayer].Color) == false)
            {
                if (PlayersArray[Game.CurrentPlayer].Name == "Computer")
                {
                    System.Timers.Timer timer = new System.Timers.Timer(200);
                    timer.Elapsed += timerElapsed;
                    timer.Start();
                }
                else
                {
                    Game.Board.UpdateGreenCellsToCurrentPlayer(PlayersArray[Game.CurrentPlayer].Color);
                    UpdatePicBoxBoard(Game.Board.Matrix);
                }

            }
            else
            {
                doWhenGameOver();
            }

        }

        private void doWhenGameOver()
        {
            PlayersArray[0].Score = Game.CountPieces(Board.k_X);
            PlayersArray[1].Score = Game.CountPieces(Board.k_O);
            StringBuilder sbWinner;

            if (PlayersArray[0].Score > PlayersArray[1].Score)
            {
                sbWinner = buildWinnerString(PlayersArray[0], PlayersArray[1]);
            }
            else
            {
                sbWinner = buildWinnerString(PlayersArray[1], PlayersArray[0]);
            }

            sbWinner.Append(Environment.NewLine);
            sbWinner.Append("Would you like another round?");
            scoreMessageBox(sbWinner);
        }
    
        private StringBuilder buildWinnerString(Player i_Winner, Player i_Looser)
        {
            StringBuilder sbWinner = new StringBuilder();
            int winnerColor = 0;

            if (i_Winner.Score != i_Looser.Score)
            {
                sbWinner.AppendFormat("{0} Won!!  ", i_Winner.Name);
                sbWinner.AppendFormat("({0} / {1}) ", i_Winner.Score, i_Looser.Score);
                WinnerPerRound.Add(i_Winner.Color);
                winnerColor = i_Winner.Color;
            }
            else
            {
                sbWinner.AppendFormat("It's a tie!!  ");
                sbWinner.AppendFormat(" ({0} / {1})", i_Winner.Score, i_Looser.Score);
                WinnerPerRound.Add(0);
                winnerColor = 0;
            }

            if (winnerColor != 0)
            {
                int counterOfWins = 0;

                foreach (int i in WinnerPerRound)
                {
                    if (i == winnerColor)
                    {
                        counterOfWins++;
                    }
                }
                sbWinner.AppendFormat("({0}/{1})", counterOfWins, WinnerPerRound.Count);
            }

            return sbWinner;
        }

        private void scoreMessageBox(StringBuilder i_MessageString)
        {
            DialogResult result = MessageBox.Show(i_MessageString.ToString(), "Othello", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                Game.Board.InitializeBoard();
                PlayersArray[0].Score = PlayersArray[1].Score = 0;
                Game.CurrentPlayer = getRandomChoiceBet0and1();
                UpdatePicBoxBoard(Game.Board.Matrix);
                startRound();
            }
            else if (result == DialogResult.No)
            {
                // End the game
                this.Close();
            }

        }

        private void timerElapsed(object sender, ElapsedEventArgs e)
        {           
            ((System.Timers.Timer)sender).Stop();
            computersTurn();
        }

        private void changeTurn()
        {
            Game.CurrentPlayer = (Game.CurrentPlayer + 1) % 2;

            if (Game.IsGameOver(PlayersArray[Game.CurrentPlayer].Color) == true)
            {
                Game.CurrentPlayer = (Game.CurrentPlayer + 1) % 2;
            }

        }

    }
}
