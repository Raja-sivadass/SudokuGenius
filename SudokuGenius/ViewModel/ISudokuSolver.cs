namespace SudokuGenius.ViewModel
{
    public interface ISudokuSolver
    {
        bool SolveGivenSoduku(int[][] ipArray, int dimensionLength);
    }
}
