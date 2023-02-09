using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OthelloGame
{
    public class ComputerPlayer
    {
        private string m_Name;
        private int m_Color;
        private readonly Random m_Rand = new Random();

        //get set
        public string NameOfComputer
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public ComputerPlayer(int color)
        {
            NameOfComputer = "Computer";
            Color = color;
        }

        private int getOponentColor()
        {
            int oponentColor = 1;
            if (Color == 1) { oponentColor = 2; }
            return oponentColor;
        }

        public void MakeMove(ref Board io_Board)
        {
            // Get a list of all valid moves
            List<Cell> validMoves = getValidMoves(io_Board);

            // If there are no valid moves, skip the turn
            if (validMoves.Count == 0)
            {
                return;
            }

            // Choose a random valid move
            Cell randomMove = validMoves[m_Rand.Next(validMoves.Count)];

            // Make the move
            getMove(ref io_Board, randomMove.Row, randomMove.Col);
        }
       
        private void getMove(ref Board io_Board, int i_Row, int i_Col)
        {
            // Place the piece on the board
            io_Board.Matrix[i_Row, i_Col] = Color;

            // Flip any opponent pieces that are sandwiched between this piece and another of the Computer's pieces
            for (int i = -1; i <= 1; i++)            {                for (int j = -1; j <= 1; j++)                {                    if (i == 0 && j == 0) { continue; }

                    flipPieces(ref io_Board, i_Row, i_Col, i, j);                }            }
        }

        // Flip the pieces of the opponent that are sandwiched between the move and another of the Computer's pieces
        private void flipPieces(ref Board io_Board, int i_Row, int i_Col, int i_RowIncrement, int i_ColIncrement)
        {
            int rowToBeFlipped = i_Row + i_RowIncrement;
            int colToBeFlipped = i_Col + i_ColIncrement;

            // Keep going in the specified direction until you reach the edge of the board or an empty space
            while (rowToBeFlipped >= 0 && rowToBeFlipped < io_Board.SizeOfBoard && colToBeFlipped >= 0 && colToBeFlipped < io_Board.SizeOfBoard && io_Board.Matrix[rowToBeFlipped, colToBeFlipped] == getOponentColor())
            {
                rowToBeFlipped += i_RowIncrement;
                colToBeFlipped += i_ColIncrement;
            }

            // If you reached the edge of the board or an empty space, return
            if (rowToBeFlipped < 0 || rowToBeFlipped >= io_Board.SizeOfBoard || colToBeFlipped < 0 || colToBeFlipped >= io_Board.SizeOfBoard || io_Board.Matrix[rowToBeFlipped, colToBeFlipped] == 0)
            {
                return;
            }

            // If you reach a piece of the Computer, flip all the pieces in between
            if (io_Board.Matrix[rowToBeFlipped, colToBeFlipped] == Color)
            {
                rowToBeFlipped -= i_RowIncrement;
                colToBeFlipped -= i_ColIncrement;

                while (rowToBeFlipped != i_Row || colToBeFlipped != i_Col)
                {
                    io_Board.Matrix[rowToBeFlipped, colToBeFlipped] = Color;
                    rowToBeFlipped -= i_RowIncrement;
                    colToBeFlipped -= i_ColIncrement;
                }
            }
        }

        private List<Cell> getValidMoves(Board i_Board)
        {
            List<Cell> validMoves = new List<Cell>();

            for (int row = 0; row < i_Board.SizeOfBoard; row++)
            {
                for (int col = 0; col < i_Board.SizeOfBoard; col++)
                {
                    // If the space is empty, check if it is a valid move
                    if (i_Board.Matrix[row, col] == 0)
                    {
                        if (isValidMove(i_Board, row, col))
                        {
                            validMoves.Add(new Cell(row, col));
                        }
                    }
                }
            }
            return validMoves;
        }

        private bool isValidMove(Board i_Board, int i_Row, int i_Col)        {            bool isValid = false;
            // Check the eight directions around the space to see if there is a player piece that can be flanked
            for (int i = -1; i <= 1; i++)            {                for (int j = -1; j <= 1; j++)                {                    if (i == 0 && j == 0) { continue; }                    if (checkDirection(i_Board, i_Row, i_Col, i, j) == true)                    {                        isValid = true;                        break;                    }                }                if (isValid == true)                {                    break;                }            }            return isValid;        }

        //// Check a direction for a valid move
        //private bool checkDirection(Board i_Board, int i_Row, int i_Col, int i_RowIncrement, int i_colIncrement)
        //{
        //    int r = i_Row + i_RowIncrement;
        //    int c = i_Col + i_colIncrement;

        //    // If the space is out of bounds or empty, return false
        //    if (r < 0 || r >= i_Board.SizeOfBoard || c < 0 || c >= i_Board.SizeOfBoard || i_Board.Matrix[r, c] == 0)
        //    {
        //        return false;
        //    }

        //    // If the space is occupied by the Computer, return false
        //    if (i_Board.Matrix[r, c] == Color)
        //    {
        //        return false;
        //    }

        //    // Keep going in the specified direction until you reach the edge of the board or an empty space
        //    while (r >= 0 && r < i_Board.SizeOfBoard && c >= 0 && c < i_Board.SizeOfBoard && i_Board.Matrix[r, c] == getOponentColor())
        //    {
        //        r += i_RowIncrement;
        //        c += i_colIncrement;
        //    }

        //    // If you reached the edge of the board or an empty space, return false
        //    if (r < 0 || r >= i_Board.SizeOfBoard || c < 0 || c >= i_Board.SizeOfBoard || i_Board.Matrix[r, c] == 0)
        //    {
        //        return false;
        //    }

        //    // If you reach a piece of the player, the move is valid
        //    if (i_Board.Matrix[r, c] == Color)
        //    {
        //        return true;
        //    }

        //    // Otherwise, the move is not valid
        //    return false;
        //}

        private bool checkDirection(Board i_Board, int i_Row, int i_Col, int i_RowIncrement, int i_ColIncrement)
        {
            int r = i_Row + i_RowIncrement;
            int c = i_Col + i_ColIncrement;
            bool hasDirection = false;
            // If the space is out of bounds or empty, return false
            if (r < 0 || r >= i_Board.SizeOfBoard || c < 0 || c >= i_Board.SizeOfBoard || i_Board.Matrix[r, c] == 0)
            {
                hasDirection = false;
            }
            else// If the space is occupied by the Computer, return false
            {
                if (i_Board.Matrix[r, c] == Color)
                {
                    hasDirection = false;
                }
                else
                {
                    // Keep going in the specified direction until you reach the edge of the board or an empty space
                    while (r >= 0 && r < i_Board.SizeOfBoard && c >= 0 && c < i_Board.SizeOfBoard && i_Board.Matrix[r, c] == getOponentColor())
                    {
                        r += i_RowIncrement;
                        c += i_ColIncrement;
                    }

                    // If you reached the edge of the board or an empty space, return false
                    if (r < 0 || r >= i_Board.SizeOfBoard || c < 0 || c >= i_Board.SizeOfBoard || i_Board.Matrix[r, c] == 0)
                    {
                        hasDirection = false;
                    }
                    else if (i_Board.Matrix[r, c] == Color)// If you reach a piece of the player, the move is valid
                    {
                        hasDirection = true;
                    }

                }               
            }
            // Otherwise, the move is not valid
            return hasDirection;
        }

    }
}
