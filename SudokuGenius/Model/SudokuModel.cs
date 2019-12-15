using SudokuGenius.ViewModel;
using System;

namespace SudokuGenius.Model
{
    public sealed class SudokuModel : ISudokuSolver
    {

        #region SingleTonInstance

        private static SudokuModel instance = null;
        private static readonly object padlock = new object();

        SudokuModel()
        {
        }

        public static SudokuModel Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SudokuModel();
                    }
                    return instance;
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// For each cell. 
        /// 1. It checks corresponding row and column.
        /// 2. It check corresponding cubic (i.e., corresponding 3*3 matrix)
        /// 3. Following backtracking search algorithm (BSA) https://en.wikipedia.org/wiki/Backtracking

        /// </summary>
        public bool SolveGivenSoduku(int[][] sbArray, int dimensionLength)
        {
            var sbRow = -1;
            var sbCol = -1;
            bool isCellEmpty = true;

            try
            {
                for (int rno = 0; rno < dimensionLength; rno++)
                {
                    for (int cno = 0; cno < dimensionLength; cno++)
                    {
                        if (sbArray[rno][cno] == 0)
                        {
                            sbRow = rno;
                            sbCol = cno;
                            isCellEmpty = false;
                            break;
                        }
                    }
                    if (!isCellEmpty)
                    {
                        break;
                    }
                }

                if (isCellEmpty)                
                    return true;                

                for (int num = 1; num <= dimensionLength; num++)
                {
                    if (DoesBasicCheckPass(sbArray, sbRow, sbCol, num))
                    {
                        sbArray[sbRow][sbCol] = num;

                        if (SolveGivenSoduku(sbArray, dimensionLength))                        
                            return true;                        
                        else                        
                            sbArray[sbRow][sbCol] = 0;                        
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool DoesBasicCheckPass(int[][] matrix, int row, int col, int num)
        {
            for (int d = 0; d < matrix.GetLength(0); d++)
            {
                if (matrix[row][d] == num)                
                    return false;                
            }

            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                if (matrix[r][col] == num)                
                    return false;
                
            }

            int valSqrt = (int)Math.Sqrt(matrix.GetLength(0));
            int boxRowStart = row - row % valSqrt;
            int boxColStart = col - col % valSqrt;

            for (int r = boxRowStart; r < boxRowStart + valSqrt; r++)
            {
                for (int d = boxColStart; d < boxColStart + valSqrt; d++)
                {
                    if (matrix[r][d] == num)                    
                        return false;                    
                }
            }
            return true;
        }

        #endregion
    }
}
