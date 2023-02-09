using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OthelloGame
{
    public class Board
    {
        private readonly int[,] m_Matrix;
        private int m_SizeOfBoard;
        public const int k_EMPTY = 0;
        public const int k_X = 1;
        public const int k_O = 2;
        public const int k_PossibleMove = 3;

        public Board(int i_SizeOfBoard)
        {
            m_SizeOfBoard = i_SizeOfBoard;
            m_Matrix = new int[m_SizeOfBoard, m_SizeOfBoard];    
            InitializeBoard();
        }

        //get set
        public int SizeOfBoard
        {
            get { return m_SizeOfBoard; }
            set { m_SizeOfBoard = value; }
        }

        public int[,] Matrix
        {
            get { return m_Matrix; }      
        }

        public void InitializeBoard()
        {
            // Set all squares to empty
            for (int i = 0; i < m_SizeOfBoard; i++)
            {
                for (int j = 0; j < m_SizeOfBoard; j++)
                {
                    m_Matrix[i, j] = k_EMPTY;
                }
            }

            // Place starting pieces
            m_Matrix[(m_SizeOfBoard / 2) - 1, (m_SizeOfBoard / 2) - 1] = k_X;
            m_Matrix[m_SizeOfBoard / 2, m_SizeOfBoard / 2] = k_X;
            m_Matrix[m_SizeOfBoard / 2, (m_SizeOfBoard / 2) - 1] = k_O;
            m_Matrix[(m_SizeOfBoard / 2) - 1, m_SizeOfBoard / 2] = k_O;
        }

        public void PlacePiece(int i_Row, int i_Col, int i_Color)
        {
            m_Matrix[i_Row, i_Col] = i_Color;
            FlipPieces(i_Row, i_Col, i_Color);
        }

        private int getOponentColor(int i_ColorOfCurrentPlayer)
        {
            int oponentColor = 1;
            if (i_ColorOfCurrentPlayer == 1) { oponentColor = 2; }
            return oponentColor;
        }

        //public bool IsValidMove(int i_Row, int i_Col, int i_Color)
        //{
            
        //    // Check if the square is already occupied
        //    if (m_Matrix[i_Row, i_Col] != Board.k_EMPTY)
        //    {
        //        return false;
        //    }

        //    // Calculate the opposite color of the piece being placed
        //    int oponentColor = getOponentColor(i_Color);

        //    // Check for pieces of the opposite color in all directions
        //    for (int iterRow = i_Row - 1; iterRow <= i_Row + 1; iterRow++)
        //    {
        //        for (int iterCol = i_Col - 1; iterCol <= i_Col + 1; iterCol++)
        //        {
        //            if (iterRow == i_Row && iterCol == i_Col)
        //            {
        //                continue;
        //            }

        //            if (iterRow >= 0 && iterRow < SizeOfBoard && iterCol >= 0 && iterCol < SizeOfBoard && m_Matrix[iterRow, iterCol] == oponentColor)
        //            {
        //                int currentRow = iterRow + (iterRow - i_Row);
        //                int currentCol = iterCol + (iterCol - i_Col);
        //                while (currentRow >= 0 && currentRow < SizeOfBoard && currentCol >= 0 && currentCol < SizeOfBoard && m_Matrix[currentRow, currentCol] == oponentColor)
        //                {
        //                    currentRow += (iterRow - i_Row);
        //                    currentCol += (iterCol - i_Col);
        //                }

        //                if (currentRow >= 0 && currentRow < SizeOfBoard && currentCol >= 0 && currentCol < SizeOfBoard && m_Matrix[currentRow, currentCol] == i_Color)
        //                {
        //                    return true;
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}

        public bool IsValidMove(int i_Row, int i_Col, int i_Color)
        {
            bool isValid = false;

            // Check if the square is already occupied
            if (m_Matrix[i_Row, i_Col] != Board.k_EMPTY)
            {
                isValid = false;
            }
            else
            {
                // Calculate the opposite color of the piece being placed
                int oponentColor = getOponentColor(i_Color);

                // Check for pieces of the opposite color in all directions
                for (int iterRow = i_Row - 1; iterRow <= i_Row + 1; iterRow++)
                {
                    for (int iterCol = i_Col - 1; iterCol <= i_Col + 1; iterCol++)
                    {
                        if (iterRow == i_Row && iterCol == i_Col)
                        {
                            continue;
                        }

                        if (iterRow >= 0 && iterRow < SizeOfBoard && iterCol >= 0 && iterCol < SizeOfBoard && m_Matrix[iterRow, iterCol] == oponentColor)
                        {
                            int currentRow = iterRow + (iterRow - i_Row);
                            int currentCol = iterCol + (iterCol - i_Col);
                            while (currentRow >= 0 && currentRow < SizeOfBoard && currentCol >= 0 && currentCol < SizeOfBoard && m_Matrix[currentRow, currentCol] == oponentColor)
                            {
                                currentRow += (iterRow - i_Row);
                                currentCol += (iterCol - i_Col);
                            }

                            if (currentRow >= 0 && currentRow < SizeOfBoard && currentCol >= 0 && currentCol < SizeOfBoard && m_Matrix[currentRow, currentCol] == i_Color)
                            {
                                isValid = true;
                                break;
                            }

                        }
                    }
                    
                    if(isValid==true)
                    {
                        break;
                    }

                }
            }

            return isValid;
        }

        public List<Cell> GetValidMoves(int i_Color)
        {
            List<Cell> validMoves = new List<Cell>();

            for (int row = 0; row < SizeOfBoard; row++)
            {
                for (int col = 0; col < SizeOfBoard; col++)
                {
                    if (IsValidMove(row, col, i_Color))
                    {
                        validMoves.Add(new Cell(row, col));
                    }

                }
            }

            return validMoves;
        }

        private bool checkDirection(int i_Row, int i_Col, int i_RowIncrement, int i_ColIncrement, int i_Color)
        {
            int r = i_Row + i_RowIncrement;
            int c = i_Col + i_ColIncrement;

            // If the space is out of bounds or empty, return false
            if (r < 0 || r >= SizeOfBoard || c < 0 || c >= SizeOfBoard || Matrix[r, c] == 0)
            {
                return false;
            }

            // If the space is occupied by the Computer, return false
            if (Matrix[r, c] == i_Color)
            {
                return false;
            }

            // Keep going in the specified direction until you reach the edge of the board or an empty space
            while (r >= 0 && r < SizeOfBoard && c >= 0 && c < SizeOfBoard && Matrix[r, c] == getOponentColor(i_Color))
            {
                r += i_RowIncrement;
                c += i_ColIncrement;
            }

            // If you reached the edge of the board or an empty space, return false
            if (r < 0 || r >= SizeOfBoard || c < 0 || c >= SizeOfBoard ||Matrix[r, c] == 0)
            {
                return false;
            }

            // If you reach a piece of the player, the move is valid
            if (Matrix[r, c] == i_Color)
            {
                return true;
            }

            // Otherwise, the move is not valid
            return false;
        }

        public void FlipPieces(int i_Row, int i_Col, int i_Color)
        {
            // Calculate the opposite color of the piece being placed
            int oppositeColor;
            if (i_Color == 1) { oppositeColor = 2; }
            else { oppositeColor = 1; }

            // Flip pieces in all directions
            for (int iterRow = i_Row - 1; iterRow <= i_Row + 1; iterRow++)
            {
                for (int iterCol = i_Col - 1; iterCol <= i_Col + 1; iterCol++)
                {
                    if (iterRow == i_Row && iterCol == i_Col)
                    {
                        continue;
                    }

                    if (iterRow >= 0 && iterRow < SizeOfBoard && iterCol >= 0 && iterCol < SizeOfBoard && m_Matrix[iterRow, iterCol] == oppositeColor)
                    {
                        int currentRow = iterRow + (iterRow - i_Row);
                        int currentCol = iterCol + (iterCol - i_Col);
                        while (currentRow >= 0 && currentRow < SizeOfBoard && currentCol >= 0 && currentCol < SizeOfBoard && m_Matrix[currentRow, currentCol] == oppositeColor)
                        {
                            currentRow += (iterRow - i_Row);
                            currentCol += (iterCol - i_Col);
                        }

                        if (currentRow >= 0 && currentRow < SizeOfBoard && currentCol >= 0 && currentCol < SizeOfBoard && m_Matrix[currentRow, currentCol] == i_Color)
                        {

                            while (currentRow != i_Row || currentCol != i_Col)
                            {
                                m_Matrix[currentRow, currentCol] = i_Color;
                                currentRow -= (iterRow - i_Row);
                                currentCol -= (iterCol - i_Col);

                            }
                        }
                    }
                }
            }
        }

        public int GetPieceAt(int i_Row, int i_Col)
        {
            return m_Matrix[i_Row, i_Col];
        }

        public void UpdateGreenCellsToCurrentPlayer(int i_CurrentPlayerColor)
        {
            List<Cell> validMoves = new List<Cell>();
            validMoves = GetValidMoves(i_CurrentPlayerColor);

            foreach( Cell currCell in validMoves)
            {
                Matrix[currCell.Row, currCell.Col] = 3;
            }
        }

        public void DeleteGreenCellsAfterMove()
        {
            // handle the previuse matrix
            for (int i = 0; i < SizeOfBoard; i++)
            {
                for (int j = 0; j < SizeOfBoard; j++)
                {
                    if (Matrix[i, j] == 3)
                    {
                        Matrix[i, j] = 0;
                    }
                }
            }
        }

    }
}
