using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class GameController
    {
        public List<Piece> Pieces { get; private set; }
        public int EmptyIndex { get; private set; }
        public TimeSpan CountdownTime { get; private set; }
        public int TotalPoint { get; private set; }
        public int CurrentLevel { get; private set; }
        private List<string[]> LevelImages { get; set; }

        public GameController()
        {
            InitializeLevels();
            CurrentLevel = 0; 
            InitializePieces(); 
            TotalPoint = 0; 
        }

      
        private void InitializeLevels()
        {
            LevelImages = new List<string[]>
            {
                new string[]
                {
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_0_0.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_0_1.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_0_2.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_1_0.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_1_1.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_1_2.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_2_0.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_2_1.png",
                    "G:\\steamIO\\App1\\App1\\Assets\\piece_2_2.png"
                },
                //new string[]
                //{
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_1.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_2.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_3.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_4.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_5.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_6.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_7.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_8.jpg",
                //    "G:\\steamIO\\App1\\App1\\Assets\\cropped_part_9.jpg"
                //}
                // Thêm nhiều màn chơi hơn ở đây
            };
        }


        public void InitializePieces()
        {
            Pieces = new List<Piece>();
            var images = LevelImages[CurrentLevel];

            for (int i = 0; i < images.Length; i++)
            {
                Pieces.Add(new Piece($"{i / 3}_{i % 3}", images[i]));
            }

            EmptyIndex = 8;
            CountdownTime = TimeSpan.FromMinutes(3);
        }


        public bool NextLevel()
        {
            if (CurrentLevel < LevelImages.Count - 1)
            {
                CurrentLevel++;
                InitializePieces();
                return true;
            }
            return false;
        }

        public void DecrementTime()
        {
            if (CountdownTime.TotalSeconds > 0)
            {
                CountdownTime = CountdownTime.Subtract(TimeSpan.FromSeconds(1));
            }
        }

        public bool IsTimeUp() => CountdownTime.TotalSeconds <= 0;

        public void ShufflePieces()
        {
            Pieces = Pieces.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public bool IsAdjacent(int index)
        {
            return (index == EmptyIndex - 1 && index % 3 != 2) ||
                   (index == EmptyIndex + 1 && index % 3 != 0) ||
                   (index == EmptyIndex - 3 || index == EmptyIndex + 3);
        }

        public void SwapPieces(int index)
        {
            var temp = Pieces[index];
            Pieces[index] = Pieces[EmptyIndex];
            Pieces[EmptyIndex] = temp;
            EmptyIndex = index;
        }

        public void AddPoints(int points)
        {
            TotalPoint += points;
        }

        public bool CheckWin()
        {
            for (int i = 0; i < Pieces.Count; i++)
            {
                string expectedName = $"{i / 3}_{i % 3}";
                if (Pieces[i].Name != expectedName)
                {
                    return false;
                }
            }
            return true;
        }
    }
}



