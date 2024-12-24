using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ClassLibrary1;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Controller;


namespace App1
{
    public sealed partial class MainWindow : Window
    {
        private readonly GameController gameController;
        private DispatcherTimer timer;
        public MainWindow()
        {
            this.InitializeComponent();
            gameController = new GameController();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, object e)
        {
            gameController.DecrementTime();
            UpdateTimerUI();

            if (gameController.IsTimeUp())
            {
                timer.Stop();
                ShowFailureDialog();
            }
        }

        private async void ShowFailureDialog()
        {
            ContentDialog failureDialog = new ContentDialog
            {
                Title = "Time's Up!",
                Content = "Bạn đã hết thời gian. Thử lại nhé!",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await failureDialog.ShowAsync();
        }

        private void UpdateTimerUI()
        {
            TimerTextBlock.Text = gameController.CountdownTime.ToString(@"mm\:ss");
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            gameController.ShufflePieces();
            RenderPuzzle();
            timer.Start();
        }

        private void nextLevelButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
           
        }
        private void RenderPuzzle()
        {
            PuzzleGrid.Children.Clear();
            PuzzleGrid.RowDefinitions.Clear();
            PuzzleGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 3; i++)
            {
                PuzzleGrid.RowDefinitions.Add(new RowDefinition());
                PuzzleGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < gameController.Pieces.Count; i++)
            {
                Button button = new Button
                {
                    Tag = i,
                    Content = (i == gameController.EmptyIndex) ? null : new Image
                    {
                        Source = new BitmapImage(new Uri(gameController.Pieces[i].ImagePath, UriKind.Relative)),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    },
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Margin = new Thickness(5)
                };

                button.Click += Piece_Click;
                Grid.SetRow(button, i / 3);
                Grid.SetColumn(button, i % 3);
                PuzzleGrid.Children.Add(button);
            }
        }

        private void UpdateTotalPointUI()
        {
            totalPoint.Text = $"Point: {gameController.totalPoint}";
        }

        private async void Piece_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int clickedIndex = (int)clickedButton.Tag;

            if (gameController.IsAdjacent(clickedIndex))
            {
                gameController.SwapPieces(clickedIndex);
                RenderPuzzle();

                if (gameController.CheckWin())
                {
                    timer.Stop(); 
                    gameController.AddPoints(10); 
                    UpdateTotalPointUI();

                    ContentDialog winDialog = new ContentDialog
                    {
                        Title = "Congratulations!",
                        Content = $"Bạn đã hoàn thành trò chơi! Điểm của bạn: {gameController.totalPoint}",
                        CloseButtonText = "OK",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await winDialog.ShowAsync();
                }
            }
        }

    }
}
