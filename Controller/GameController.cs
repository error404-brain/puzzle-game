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

        public int totalPoint { get; private set; }

        public GameController()
        {
            InitializePieces();
            totalPoint = 0;
        }

        public void InitializePieces()
        {
            Pieces = new List<Piece>
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

            EmptyIndex = 8;
            CountdownTime = TimeSpan.FromMinutes(1);
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
            totalPoint += points;
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

