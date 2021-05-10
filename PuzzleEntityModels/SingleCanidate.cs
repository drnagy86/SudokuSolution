using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLogic
{
    public class SingleCanidate
    {
        private int[] _coordinates = new int[2];
        private List<int> _possibleAnswers = new List<int>();

        public SingleCanidate(int[] coordinates, List<int> possibleAnswers)
        {
            _coordinates = coordinates;
            _possibleAnswers = possibleAnswers;
        }

        public int[] Coordinates
        {
            get { return _coordinates; }
            set { _coordinates = value; }
        }

        public List<int> PossibleAnswers
        {
            get { return _possibleAnswers; }
            set { _possibleAnswers = value; }
        }

    }
}
