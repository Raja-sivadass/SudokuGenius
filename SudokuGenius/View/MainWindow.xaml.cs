using SudokuGenius.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace SudokuGenius.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new SudokuGeniusViewModel();
        }

        private void Onload(object sender, RoutedEventArgs e)
        {
            ResetBoard();
        }

        private void UploadPuzzle(object sender, RoutedEventArgs e)
        {
            try
            {
                var vmObj = (this.DataContext) as SudokuGeniusViewModel;

                var fileDialog = new System.Windows.Forms.OpenFileDialog();
                var result = fileDialog.ShowDialog();
                switch (result)
                {
                    case System.Windows.Forms.DialogResult.OK:                        
                        var txtFile = File.ReadAllLines(fileDialog.FileName);
                        if (txtFile == null)
                        {
                            MessageBox.Show("Invalid input file.");
                        }
                        else
                        {
                            int[][] sbArray = txtFile.Select(l => l.Split(',')
                                                    .Select(i => int.Parse(i)).ToArray()).ToArray();

                            lst.ItemsSource = sbArray;
                            vmObj.PuzzleCollection = sbArray;
                        }

                        txblock_time.Text = "Time taken to solve : ";
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid input file.");
            }
        }

        private void ResetCellsHandler(object sender, RoutedEventArgs e)
        {
            var vmObj = (this.DataContext) as SudokuGeniusViewModel;
            vmObj.PuzzleCollection = null;

            ResetBoard();
        }

        private void ResetBoard()
        {
            try
            {
                List<List<string>> lsts = new List<List<string>>();

                for (int i = 0; i < 9; i++)
                {
                    lsts.Add(new List<string>());

                    for (int j = 0; j < 9; j++)
                    {
                        lsts[i].Add("");
                    }
                }
                lst.ItemsSource = lsts;
                txblock_time.Text = "Time taken to solve : ";
            }
            catch { }
        }

    }
}
