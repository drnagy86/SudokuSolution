using PuzzleEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLogic
{
    public class Solver
    {

        // *** NOTE
        // This class is not fully implemented. Ran out of time. Somthing that could be an extra
        // feature some day with more work/ experience coding.




        // the canidate list from the setup puzzle object
        // the puzzle to try and solve from the setup
        // bool that can say if the puzzle can be solved(one for every method that solves the puzzle)
        // puzzle difficulty object? contains number of each kind of method of solution, holds a value for easy, medium, hard

        private PuzzleNine _puzzleObject = new PuzzleNine(1);
        private List<SingleCanidate> _canidateList = new List<SingleCanidate>();
        public bool isSolvable { get; private set; }
        private List<int[]> _blankCoordinates = new List<int[]>();
        private const int _puzzleSize = 9;
        private Difficulty _difficulty = new Difficulty();

        public Solver(PuzzleNine puzzle)
        {
            // make sure that it is creating a new object and not passing a reference            
            _puzzleObject = puzzle.CopyPuzzle();
            _puzzleObject.ResetCoordinates();
            _puzzleObject.UpdateCoordinates();

            findPossibleAnswers();
            SinglePosition();

        }

        public Difficulty Difficulty
        {
            get { return _difficulty; }            
        }

        public void XWing()
        {
            // two lines, each has the same two positions for a number
            // eliminate from other squares
            // Conditions:
            // - a canidate is in at least four different positions
            // - a canidate shares a row with itself 
            // - a canidate shares a column with itself
            // - the shared coordinates make a rectangle

            findPossibleAnswers();

            List<SingleCanidate> findXWing = new List<SingleCanidate>();

            // - a canidate is in at least four different positions
            for (int testNum = 1; testNum <= _puzzleSize; testNum++)
            {
                List<SingleCanidate> testCanidates = new List<SingleCanidate>();
                int count = 0;

                foreach (SingleCanidate canidate in _canidateList)
                {
                    if (canidate.PossibleAnswers.Contains(testNum))
                    {
                        testCanidates.Add(canidate);
                        count++;
                    }
                    int index = _canidateList.IndexOf(canidate);
                    // if I am on the last canidate
                    if (_canidateList.Count == index && count >= 4)
                    {
                        //move testHolder to next test
                        canidateSharesRow(testCanidates, testNum);
                    }
                }
            }         
        }

        private void canidateSharesRow(List<SingleCanidate> testHolder, int testNum)
        {
            // - a canidate shares a row with itself
            // is number one in this row twice

            List<int[]> coordList = new List<int[]>();
            List<int> xValues = new List<int>();
            List<int> yValues = new List<int>();

            int rowCount = 0;
            int colCount = 0;


            foreach (SingleCanidate canidate in testHolder)
            {
                coordList.Add(canidate.Coordinates);                
            }

            foreach (int[] coord in coordList)
            {
                xValues.Add(coord[0]);
                yValues.Add(coord[1]);
            }

            for (int i = 0; i < xValues.Count/2; i++)
            {
                for (int j = i+1; j < xValues.Count; j++)
                {
                    if (xValues[i] == xValues[j])
                    {
                        rowCount++;
                    }
                    if (yValues[i] == yValues[j])
                    {
                        colCount++;
                    }
                }
            }

            if (rowCount >= 2 & colCount >= 2)
            {
                // see if coordinates make a rectangle
                coordsMakeARect(coordList);
            }
        }

        public void coordsMakeARect(List<int[]> coordList)
        {           
            // this is where I got stuck and decieded that this project was good enough
            foreach (int[] coord in coordList)
            {
                List<int[]> row1 = new List<int[]>();

                for (int i = 0; i < coordList.Count; i++)
                {
                    for (int j = i + 1; j < coordList.Count; j++)
                    {
                        int[] coord1 = coordList[i];
                        int[] coord2 = coordList[j];

                        int x1 = coord1[0];
                        int x2 = coord2[0];

                        if (x1 == x2)
                        {
                            row1.Add(coord2);
                        }
                    }
                }
            }
        }


        public void SinglePosition()
        {
            // Goal of first solver:
            // Find where there can be only one answer, add that answer to the puzzle,
            // update the rows and columns that are affected in the canidate list           

            // keep track of times that singlePosition was run

            bool flag = true;            

            while (flag)
            {
                // need to not be working off coord count
                int firstCount = _blankCoordinates.Count;
                foreach (SingleCanidate canidate in _canidateList)
                {
                    // add to the puzzle where there is only one possible answer
                    int[] xyCoords = canidate.Coordinates;
                    List<int> possibleAnswers = canidate.PossibleAnswers;

                    if (possibleAnswers.Count == 1)
                    {
                        // get the only answer and add to the puzzle
                        _puzzleObject._puzzle[xyCoords[0], xyCoords[1]] = possibleAnswers[0];
                        //add to counter
                        _difficulty.singlePosition++;
                        
                    }
                }

                //trash the old canidate list and start fresh
                _canidateList = new List<SingleCanidate>();
                
                // update canidate list
                _puzzleObject.UpdateCoordinates();
                updateAvailableCoordinates();

                if (_blankCoordinates.Count == 0)
                {
                    // the puzz is solved                   
                    _difficulty.isSolvedSingPos = true;
                    flag = false;
                } 
                else if (firstCount == _blankCoordinates.Count)
                {
                    //compare to the count of canidate list at the begining to see if there was a change.
                    //This means that this method of solving the puzzle has reached it's limit.
                    _difficulty.isSolvedSingPos = false;
                    flag = false;
                }
                else
                {
                    //find the possible answers
                    findPossibleAnswers();
                }

                // if there are more canidates, run it again
                if (_canidateList.Count > 0)
                {
                    //_difficulty.isSolvedSingPos = false;
                    SinglePosition();
                }

            }
        }

        private void isSolvableMethod()
        {
            // to do: this method likely belongs in the setup puzzle
           isSolvable = true;

            foreach (SingleCanidate canidate in _canidateList)
            {
                int[] xyCoords = canidate.Coordinates;
                List<int> possibleAnswers = canidate.PossibleAnswers;

                if (possibleAnswers.Count == 0)
                {
                    isSolvable = false;                    
                }
            }          


        }

        private void updateAvailableCoordinates()
        {

            _blankCoordinates = new List<int[]>();

            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    //keep an array to hold the position
                    int[] position = new int[2];
                    //using the test table as a reference, make a list of all the available positions
                    if (_puzzleObject.CoordinateTable[i, j])
                    {
                        //row
                        position[0] = i;
                        //column
                        position[1] = j;

                        _blankCoordinates.Add(position);
                    }
                }
            }

        }

        private void findPossibleAnswers()
        {
            //update the blank coordinates table
            _puzzleObject.UpdateCoordinates();
            _blankCoordinates = _puzzleObject.FindTrueCoordinateTable();

            //get a list of available numbers for the sqaure  
            foreach (int[] xyCoords in _blankCoordinates)
            {
                // find all the possible numbers that could go there
                List<int> possibleCanidates = new List<int>();

                for (int i = 1; i < _puzzleSize + 1; i++)
                {
                    possibleCanidates.Add(i);
                }

                //starting at my x and y coordinates
                int x = xyCoords[0]; 
                int y = xyCoords[1]; 

                // check every other column in the row besides itself
                for (int col = 0; col < _puzzleSize; col++)
                {
                    if (possibleCanidates.Contains(_puzzleObject._puzzle[x, col]))
                    {
                        possibleCanidates.Remove(_puzzleObject._puzzle[x, col]);
                    }
                }

                //check every row at the column besides itself
                for (int row = 0; row < _puzzleSize; row++)
                {
                    if (possibleCanidates.Contains(_puzzleObject._puzzle[row, y]))
                    {
                        possibleCanidates.Remove(_puzzleObject._puzzle[row, y]);
                    }
                }

                // check the box...
                List<int[]> checkTheseBoxPositions = new List<int[]>();

                // this contains a list of 4 coordiates to check
                checkTheseBoxPositions = whichBoxCoords(xyCoords);

                foreach (int[] position in checkTheseBoxPositions)
                {
                    if (possibleCanidates.Contains(_puzzleObject._puzzle[position[0], position[1]]))
                    {
                        //remove this value from the possible canidates list
                        possibleCanidates.Remove(_puzzleObject._puzzle[position[0], position[1]]);
                    }
                }

                SingleCanidate singleCanidate = new SingleCanidate(xyCoords, possibleCanidates);
                _canidateList.Add(singleCanidate);

            }
        }

        private List<int[]> whichBoxCoords(int[] coordsToTest)
        {
            List<int[]> boxCoords = new List<int[]>();

            int row = coordsToTest[0];      //3
            int column = coordsToTest[1];   //2

            // box boundries
            bool boxTopLeft = (row == 0 || row == 1 || row == 2) && (column == 0 || column == 1 || column == 2);
            bool boxTopMid = (row == 0 || row == 1 || row == 2) && (column == 3 || column == 4 || column == 5);
            bool boxTopRight = (row == 0 || row == 1 || row == 2) && (column == 6 || column == 7 || column == 8);
            bool boxMidLeft = (row == 3 || row == 4 || row == 5) && (column == 0 || column == 1 || column == 2);
            bool boxMidMid = (row == 3 || row == 4 || row == 5) && (column == 3 || column == 4 || column == 5);
            bool boxMidRight = (row == 3 || row == 4 || row == 5) && (column == 6 || column == 7 || column == 8);
            bool boxBottomLeft = (row == 6 || row == 7 || row == 8) && (column == 0 || column == 1 || column == 2);
            bool boxBottomMid = (row == 6 || row == 7 || row == 8) && (column == 3 || column == 4 || column == 5);
            bool boxBottomRight = (row == 6 || row == 7 || row == 8) && (column == 6 || column == 7 || column == 8);

            List<int> rowCoords = new List<int>();
            List<int> colCoords = new List<int>();



            if (boxTopLeft)
            {
                for (int i = 0; i < 3; i++)
                {
                    rowCoords.Add(i);
                    colCoords.Add(i);
                }
            }
            else if (boxTopMid)
            {
                for (int rows = 0; rows < 3; rows++)
                {
                    rowCoords.Add(rows);
                }
                for (int cols = 3; cols < 6; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            else if (boxTopRight)
            {
                for (int rows = 0; rows < 3; rows++)
                {
                    rowCoords.Add(rows);
                }

                for (int cols = 6; cols < 9; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            // seccond row of boxes
            else if (boxMidLeft)
            {
                for (int rows = 3; rows < 6; rows++)
                {
                    rowCoords.Add(rows);
                }

                for (int cols = 0; cols < 3; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            else if (boxMidMid)
            {
                for (int rows = 3; rows < 6; rows++)
                {
                    rowCoords.Add(rows);
                }

                for (int cols = 3; cols < 6; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            else if (boxMidRight)
            {
                for (int rows = 3; rows < 6; rows++)
                {
                    rowCoords.Add(rows);
                }

                for (int cols = 6; cols < 9; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            // row 3 of boxes
            else if (boxBottomLeft)
            {
                for (int rows = 6; rows < 9; rows++)
                {
                    rowCoords.Add(rows);
                }

                for (int cols = 0; cols < 3; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            else if (boxBottomMid)
            {
                for (int rows = 6; rows < 9; rows++)
                {
                    rowCoords.Add(rows);
                }
                for (int cols = 3; cols < 6; cols++)
                {
                    colCoords.Add(cols);
                }
            }
            else if (boxBottomRight)
            {
                for (int rows = 6; rows < 9; rows++)
                {
                    rowCoords.Add(rows);
                }

                for (int cols = 6; cols < 9; cols++)
                {
                    colCoords.Add(cols);
                }
            }

            // remove the test coords from rowCoords and colCoords
            rowCoords.Remove(row);
            colCoords.Remove(column);

            int[] one = { rowCoords[0], colCoords[0] };
            int[] two = { rowCoords[0], colCoords[1] };
            int[] three = { rowCoords[1], colCoords[0] };
            int[] four = { rowCoords[1], colCoords[1] };

            boxCoords.Add(one);
            boxCoords.Add(two);
            boxCoords.Add(three);
            boxCoords.Add(four);


            return boxCoords;

        }
    }
}
