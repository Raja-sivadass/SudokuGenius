using SudokuGenius.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace SudokuGenius.ViewModel
{
    public class SudokuGeniusViewModel : INotifyPropertyChanged
    {

        #region Properties

        private int[][] puzzleCollection;
        public int[][] PuzzleCollection {
            get { return puzzleCollection; }
            set
            {
                if (value != puzzleCollection)
                {
                    puzzleCollection = value;
                    RaiseProperChanged();
                }
            }
        }



        private int[][] resultCollection;
        public int[][] ResultCollection
        {
            get { return resultCollection; }
            set
            {
                if (value != resultCollection)
                {
                    resultCollection = value;
                    RaiseProperChanged();
                }
            }
        }
        private string timeTaken;
        public string TimeTaken
        {
            get
            {
                return timeTaken;
            }
            set
            {
                timeTaken = value;
                RaiseProperChanged();
            }
        }

        #endregion

        #region Command

        public RelayCommand SolvePuzzleCommand { get; set; }
        public RelayCommand LoadDefaultPuzzleCommand { get; set; }

        #endregion

        #region Constructor

        public SudokuGeniusViewModel()
        {
            SolvePuzzleCommand = new RelayCommand((o) => SolvePuzzleCommandExecute(o));
            LoadDefaultPuzzleCommand = new RelayCommand((o) => LoadDefaultPuzzleCommandExecute(o));
        }

        #endregion

        #region CommandMethods

        private void SolvePuzzleCommandExecute(object obj)
        {
            try
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                
                int[][] sbArray = PuzzleCollection;
                if (sbArray == null) sbArray = new int[9][] {new int[]{0,0,0,0,0,0,0,0,0 }, new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 } , new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
                int nCount = sbArray.GetLength(0);

                ISudokuSolver solver = SudokuModel.Instance;
                if (solver.SolveGivenSoduku(sbArray, nCount))
                {
                    List<List<string>> lsts = new List<List<string>>();
                    for (int i = 0; i < 9; i++)
                    {
                        lsts.Add(new List<string>());
                        for (int j = 0; j < 9; j++)
                        {
                            lsts[i].Add(sbArray[i][j].ToString());
                        }
                    }
                    var itemCtrl = obj as ItemsControl;
                    itemCtrl.ItemsSource = lsts;
                }

                watch.Stop();
                TimeTaken = "Time taken to solve : " + watch.ElapsedMilliseconds + " (in milliseconds)";
            }
            catch { }
        }

        private void LoadDefaultPuzzleCommandExecute(object obj)
        {
            try
            {
                TimeTaken = "Time taken to solve : " ;
                var sbArray =  new int[9][] {new int[]{0,0,0,5,3,4,0,8,0 }, new int[]{ 0, 8, 0, 0, 1, 0, 4, 0, 0 }, new int[]{ 0, 2, 0, 8, 0, 0, 0, 7, 1 },
                                                       new int[] { 8, 0, 0, 0, 6, 0, 0, 5, 0 },new int[] { 4, 0, 0, 0, 0, 5, 8, 3, 0 },new int[] { 6, 3, 0, 1, 0, 0, 0, 0, 0 },
                                                       new int[] { 0, 0, 0, 0, 0, 1, 3, 0, 0 },new int[] { 0, 0, 0, 0, 7, 0, 0, 0, 0 } ,new int[]{ 0, 1, 6, 2, 0, 0, 0, 0, 0 } };
                
                List<List<string>> lsts = new List<List<string>>();
                for (int i = 0; i < 9; i++)
                {
                    lsts.Add(new List<string>());
                    for (int j = 0; j < 9; j++)
                    {
                        lsts[i].Add(sbArray[i][j] == 0 ? "" : sbArray[i][j].ToString());
                    }
                }

                var itemCtrl = obj as ItemsControl;
                itemCtrl.ItemsSource = lsts;
                puzzleCollection = sbArray;                
            }
            catch { }
        }

        #endregion

        #region EventHandler

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaiseProperChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #endregion
    }
}
