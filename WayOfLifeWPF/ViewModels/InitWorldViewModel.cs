using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WayOfLifeWPF.Models;

namespace WayOfLifeWPF.ViewModels
{
    public class InitWorldViewModel
    {
        private Grid GameOfLifeGrid { get; set; }
        private GameOfLifeViewModel GameOfLifeViewModel { get; set; }
        private World World { get; set; }
        private Random Ran { get; set; }
        private int RectangleCount { get; set; }
        private int RectSize { get; set; }

        public InitWorldViewModel(Grid gameOfLifeGrid)
        {
            RectSize = 3;
            GameOfLifeGrid = gameOfLifeGrid;

            World = new World(75, 75);
            Ran = new Random();

            CreateGrid();
            GameOfLifeViewModel = new GameOfLifeViewModel(World);
        }

        public void Start()
        {
            GameOfLifeViewModel.Start();
        }

        public void Stop() 
        {
            GameOfLifeViewModel.Running = false;
        }

        private void CreateGrid()
        {
            for (int i = 0; i < World.Grid2DArray.GetLength(0); i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                GameOfLifeGrid.ColumnDefinitions.Add(column);

                for (int j = 0; j < World.Grid2DArray.GetLength(1); j++)
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = new GridLength(10);
                    GameOfLifeGrid.RowDefinitions.Add(row);

                    RandomWorld(i, j);
                }
            }
        }

        private void RandomWorld(int i, int j)
        {
            Rectangle rect;
            if (Ran.Next(14) < 1)
            {
                rect = new Rectangle { Height = RectSize, Width = RectSize, Fill = new SolidColorBrush(Colors.White), Visibility = Visibility.Hidden};
                World.Grid2DArray[i, j] = true;
            }
            else
            {
                rect = new Rectangle { Height = RectSize, Width = RectSize, Fill = new SolidColorBrush(Colors.White), Visibility = Visibility.Hidden };
                World.Grid2DArray[i, j] = false;
            }

            Grid.SetRow(rect, i);
            Grid.SetColumn(rect, j);
            GameOfLifeGrid.Children.Add(rect);
            World.Grid[RectangleCount] = rect;
            RectangleCount++;
        }
    }
}