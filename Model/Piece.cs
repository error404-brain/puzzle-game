using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary1
{
    public class Piece
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public Piece(string name, string imagePath)
        {
            Name = name;
            ImagePath = imagePath;
        }
    }

}
