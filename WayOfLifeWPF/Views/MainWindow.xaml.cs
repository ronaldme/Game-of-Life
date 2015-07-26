using System;
using System.Windows;
using WayOfLifeWPF.ViewModels;

namespace WayOfLifeWPF
{
    public partial class MainWindow
    {
        private InitWorldViewModel InitWorldViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            InitWorldViewModel = new InitWorldViewModel(GameOfLifeGrid);
            DataContext = InitWorldViewModel;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            InitWorldViewModel.Stop();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            InitWorldViewModel.Start();
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            // Sort of a reset
            new MainWindow().Show();
            Close();
        }
    }
}