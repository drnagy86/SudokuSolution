using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using PuzzleEntityModels;

namespace ControlLogic
{
    public class SetupPuzzle
    {
        private const int _puzzleSize = 9;
        public PuzzleNine puzzleSolution { get; set; }
        public PuzzleNine setupPuzzle { get; set; }
        private Canidates _canidateList = new Canidates();
        private Random _random = new Random();
        private List<int[]> _blankCoordinates = new List<int[]>();

        public SetupPuzzle()
        {

        }

        public SetupPuzzle(int difficultySelection)
        {
            puzzleSolution = new PuzzleNine();

            //copy the puzzle
            setupPuzzle = puzzleSolution.CopyPuzzle();            

            // take numbers out of the puzzle
            buildPuzzle(difficultySelection);

            setupPuzzle.UpdateCoordinates();
        }

        public PuzzleNine LoadSavedPuzzle()
        {
            List<int[,]> puzzlesToLoad = null;
            PuzzleNine userAnswers = new PuzzleNine(1);

            try
            {
                DataAccessor dataAccessor = new DataAccessor();
                puzzlesToLoad = dataAccessor.RetrieveSaveSolAndSetup();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Data not availiable.", ex);
            }

            if (puzzlesToLoad != null)
            {
                // save data contains a solution, a setup, and user answers

                puzzleSolution = new PuzzleNine();
                puzzleSolution._puzzle = puzzlesToLoad[0];

                setupPuzzle = puzzleSolution.CopyPuzzle();

                //basically the same as the buildPuzzle method
                setupPuzzle._puzzle = puzzlesToLoad[1];

                UpdateCanidates();                

                userAnswers._puzzle = puzzlesToLoad[2];
            }

            return userAnswers;

        }

        public Canidates CanidateList
        {
            get {return _canidateList; }
            set { _canidateList = value; }
        }

        public void UpdateSetupCoordiateTable()
        {
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    if (setupPuzzle._puzzle[i, j] == 0)
                    {
                        setupPuzzle.CoordinateTable[i, j] = true;
                    }
                    if (setupPuzzle._puzzle[i, j] != 0)
                    {
                        setupPuzzle.CoordinateTable[i, j] = false;
                    }
                }
            }
        }

        private void buildPuzzle(int difficultySelection)
        {
            // remove the center
            setupPuzzle._puzzle[4, 4] = 0;

            // single canidate can't solve the puzzle at 30 iterations with random seed 1
            // to move past easy difficulty, add 10-20 to counter
            int counter = 0;

            if (difficultySelection == 1)
            {
                // medium
                counter = 20;
            }
            else if (difficultySelection == 2)
            {
                // hard
                counter = 25;
            }
            else if (difficultySelection == 3)
            {
                // insane
                counter = 30;
            }
            else
            {
                //easy
                counter = 15;
            }            

            do
            {
                // get random coordinates
                int[] randomCoord = randomCoordinates();

                setupPuzzle.ResetCoordinates();
                setupPuzzle.UpdateCoordinates();

                //find all the places that have been replaced already
                // list of coordinates based on what is taken

                _blankCoordinates = setupPuzzle.FindTrueCoordinateTable();

                foreach (int[] coord in _blankCoordinates)
                {
                    if (randomCoord == coord)
                    {
                        // need a new coordinate
                        randomCoord = randomCoordinates();
                    }
                }
                //take out value at random coords
                setupPuzzle._puzzle[randomCoord[0], randomCoord[1]] = 0;
                //take out it's mirror
                setupPuzzle._puzzle[8 - randomCoord[0], 8 - randomCoord[1]] = 0;

                //always start with easy loaded but take things out as it goes
                //apply the solver algorithms here
                Solver solver = new Solver(setupPuzzle);

                if (difficultySelection == 1)
                {
                    if (!solver.Difficulty.isSolvedSingPos)
                    {
                        //medium, stop when it can't be solved by single solution

                        counter = 1;

                        //NOTE: Not fully implemented. Add on feature some day.

                        // hard
                        // add another solver

                        // insane
                        // stop when only randomly choosing the positon works
                        // (which maybe doesn't really need a solver)


                        // another way of dialing this in could be number of moves needed to complete and
                        // types of ways to solve the puzzle

                    }
                }



                counter--;
            } while (counter > 0);

            UpdateCanidates();
        }

        public void UpdateCanidates()
        {

            UpdateSetupCoordiateTable();
            _blankCoordinates = setupPuzzle.FindTrueCoordinateTable();

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
                int x = xyCoords[0]; //1
                int y = xyCoords[1]; //2

                // check every other column in the row besides itself
                for (int col = 0; col < _puzzleSize; col++)
                {
                    if (possibleCanidates.Contains(setupPuzzle._puzzle[x, col]))
                    {
                        possibleCanidates.Remove(setupPuzzle._puzzle[x, col]);
                    }
                }

                //check every row at the column besides itself
                for (int row = 0; row < _puzzleSize; row++)
                {
                    if (possibleCanidates.Contains(setupPuzzle._puzzle[row, y]))
                    {
                        possibleCanidates.Remove(setupPuzzle._puzzle[row, y]);
                    }
                }

                // check the box...
                List<int[]> checkTheseBoxPositions = new List<int[]>();

                // this contains a list of 4 coordiates to check
                checkTheseBoxPositions = whichBoxCoords(xyCoords);

                foreach (int[] position in checkTheseBoxPositions)
                {
                    if (possibleCanidates.Contains(setupPuzzle._puzzle[position[0], position[1]]))
                    {
                        //remove this value from the possible canidates list
                        possibleCanidates.Remove(setupPuzzle._puzzle[position[0], position[1]]);
                    }
                }

                SingleCanidate singleCanidate = new SingleCanidate(xyCoords, possibleCanidates);

                _canidateList.AddCanidate(singleCanidate);
            }
        }

        private int[] randomCoordinates()
        {
            int[] randomCoords = { _random.Next(0, _puzzleSize), _random.Next(0, _puzzleSize )};
            return randomCoords;            
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
                for (int i = 0 ; i < 3; i++)
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
