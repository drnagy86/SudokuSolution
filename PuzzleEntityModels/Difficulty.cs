using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleEntityModels
{
    public class Difficulty
    {
        // holds the number of times a solution was run on a puzzle

        public int singlePosition { get; set; }
        public bool isSolvedSingPos { get; set; }

        public Difficulty()
        {
            isSolvedSingPos = false;
        }

    }

    enum DifficultyRating
    {
        Easy,
        Intermediate,
        Hard,
        Intense
    }
}
