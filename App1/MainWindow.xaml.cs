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

namespace App1
{
    public sealed partial class MainWindow : Window
    {
        private List<Piece> pieces;
        private int emptyIndex;

        public MainWindow()
        {
            this.InitializeComponent();
            this.InitializePieces();
        }

        private void InitializePieces()
        {
            pieces = new List<Piece>
            {
                new Piece("0_0", "G:\\steamIO\\App1\\App1\\Assets\\piece_0_0.png"),
                new Piece("0_1", "G:\\steamIO\\App1\\App1\\Assets\\piece_0_1.png"),
                new Piece("0_2", "G:\\steamIO\\App1\\App1\\Assets\\piece_0_2.png"),
                new Piece("1_0", "G:\\steamIO\\App1\\App1\\Assets\\piece_1_0.png"),
                new Piece("1_1", "G:\\steamIO\\App1\\App1\\Assets\\piece_1_1.png"),
                new Piece("1_2", "G:\\steamIO\\App1\\App1\\Assets\\piece_1_2.png"),
                new Piece("2_0", "G:\\steamIO\\App1\\App1\\Assets\\piece_2_0.png"),
                new Piece("2_1", "G:\\steamIO\\App1\\App1\\Assets\\piece_2_1.png"),
                new Piece("2_2", "G:\\steamIO\\App1\\App1\\Assets\\piece_2_2.png"),
            };

            emptyIndex = 8;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ShufflePieces();
            RenderPuzzle();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void ShufflePieces()
        {
            pieces = pieces.OrderBy(x => Guid.NewGuid()).ToList();
        }

        private void RenderPuzzle()
        {
            PuzzleGrid.Children.Clear();
            PuzzleGrid.RowDefinitions.Clear();
            PuzzleGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 3; i++)
            {
                var rowDef = new RowDefinition { Height = new Microsoft.UI.Xaml.GridLength(1, Microsoft.UI.Xaml.GridUnitType.Star) };
                var colDef = new ColumnDefinition { Width = new Microsoft.UI.Xaml.GridLength(1, Microsoft.UI.Xaml.GridUnitType.Star) };

                PuzzleGrid.RowDefinitions.Add(rowDef);
                PuzzleGrid.ColumnDefinitions.Add(colDef);
            }

            for (int i = 0; i < pieces.Count; i++)
            {
                Button button = new Button
                {
                    Tag = i,
                    Content = (i == emptyIndex) ? null : new Image { Source = new BitmapImage(new Uri(pieces[i].ImagePath, UriKind.Relative)), HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch },
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Margin = new Microsoft.UI.Xaml.Thickness(5) 
                };
                button.Click += Piece_Click;
                Grid.SetRow(button, i / 3);
                Grid.SetColumn(button, i % 3);
                PuzzleGrid.Children.Add(button);
            }
        }


        private bool CheckWin()
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                string expectedName = $"{i / 3}_{i % 3}";
                if (pieces[i].Name != expectedName)
                {
                    return false;
                }
            }
            return true;
        }


        private async void Piece_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            int clickedIndex = (int)clickedButton.Tag;

            if (IsAdjacent(clickedIndex))
            {
                SwapPieces(clickedIndex);
                RenderPuzzle();
                if (CheckWin())
                {
                    ContentDialog winDialog = new ContentDialog
                    {
                        Title = "Congratulations!",
                        Content = "Duma mày đã hoàn thành game!",
                        CloseButtonText = "OK"
                    };
                    winDialog.XamlRoot = this.Content.XamlRoot;
                    await winDialog.ShowAsync();
                }
            }
        }





        private bool IsAdjacent(int index)
        {
            return (index == emptyIndex - 1 && index % 3 != 2) ||
                   (index == emptyIndex + 1 && index % 3 != 0) ||
                   (index == emptyIndex - 3 || index == emptyIndex + 3);
        }

        private void SwapPieces(int index)
        {
            var temp = pieces[index];
            pieces[index] = pieces[emptyIndex];
            pieces[emptyIndex] = temp;
            emptyIndex = index;
        }
    }
}
