using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLogic
{
    public class Canidates
    {
        private List<SingleCanidate> _canidateList = new List<SingleCanidate>();
        private int _puzzleSize = 9;

        public Canidates()
        {
            _canidateList = new List<SingleCanidate>();
        }

        public List<SingleCanidate> CanidateList
        {
            get { return _canidateList; }
            set { _canidateList = value; }
        }


        public bool SavePencilMarks(List<SingleCanidate> pencilMarks)
        {
            bool result = false;

            // break pencilMarks apart so that it can be saved
            //xy,12345
            //xy,23
            //xy, 1

            List<string[]> brokenUpPencilMarks = new List<string[]>();

            foreach (SingleCanidate mark in pencilMarks)
            {
                string[] coordsMarks = new string[2];
                coordsMarks[0] = mark.Coordinates[0].ToString() + mark.Coordinates[1].ToString();

                foreach (int canidate in mark.PossibleAnswers)
                {
                    coordsMarks[1] += canidate.ToString();
                }
                brokenUpPencilMarks.Add(coordsMarks);
            }
            
            try
            {
                DataAccessor dataAccessor = new DataAccessor();
                dataAccessor.SavePencilMarks(brokenUpPencilMarks);
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Your save location could not be created", ex);
            }
            return result;
        }

        public void LoadPencilMarks()
        {            
            List<string[]> pencilMarksToLoad = new List<string[]>();

            try
            {
                DataAccessor dataAccessor = new DataAccessor();
                pencilMarksToLoad = dataAccessor.RetrievePencilMarks();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not availiable.", ex);
            }

            if (pencilMarksToLoad != null)
            {
                foreach (string[] pm in pencilMarksToLoad)
                {
                    int[] coords = new int[2];
                    List<int> marks = new List<int>();

                    //coords = pencilMarksToLoad[0];
                    string coordsString = pm[0];

                    // (int)Char.GetNumericValue(contentList[0])
                    coords[0] = (int)Char.GetNumericValue(coordsString[0]);
                    coords[1] = (int)Char.GetNumericValue(coordsString[1]);

                    //marks = pencilMarksToLoad[1];
                    foreach (char num in pm[1])
                    {
                        marks.Add((int)Char.GetNumericValue(num));
                    }

                    SingleCanidate pencilMark = new SingleCanidate(coords, marks);
                    _canidateList.Add(pencilMark);
                }
            }
        }

        public void AddCanidate(SingleCanidate canidate)
        {
            _canidateList.Add(canidate);
        }

        public void RemoveCanidateByCoords(int[] coords)
        {
            int index = IndexOfCanidate(coords);
            _canidateList.RemoveAt(index);
        }


        public int[] CoordsRight(int[] currentCoords)
        {            
            int[] right = { 0, 0 };

            int index = IndexOfCanidate(currentCoords);

            if (index == _canidateList.Count - 1)
            {                
                //if it is at the end of the whole list
                right = _canidateList[0].Coordinates;                
            }
            else
            {                
                right = _canidateList[index + 1].Coordinates;
            }
            return right;
        }

        public int[] CoordsLeft(int[] currentCoords)
        {            
            int[] left = { 0, 0 };
            int index = IndexOfCanidate(currentCoords);

            if (index == 0)
            {
                //it is at the begining of the list, go to the end
                left = _canidateList[_canidateList.Count - 1].Coordinates;
            }
            else
            {
                left = _canidateList[index - 1].Coordinates;
            }
            return left;
        }

        public int IndexOfCanidate(int[] currentCoords)
        {            
            int index = -1;

            for (int i = 0; i < _canidateList.Count; i++)
            {
                if (_canidateList[i].Coordinates[0] == currentCoords[0] && 
                    _canidateList[i].Coordinates[1] == currentCoords[1])
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        

    }
}
