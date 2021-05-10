using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleEntityModels
{
    public class PuzzleNine
    {
        public int[,] _puzzle { get; set; }
        private const int _puzzleSize = 9;
        private bool[,] _coordinateTable = new bool[_puzzleSize, _puzzleSize];
        private Random _random = new Random();

        public bool[,] CoordinateTable
        {
            get { return _coordinateTable; }
        }

        public PuzzleNine()
        {   
            bool badPuzzle;
            // make sure that the puzzle is able to be solved, otherwise make a new one
            do
            {
                badPuzzle = false;
                makeNewPuzzle();

                for (int i = 0; i < _puzzleSize; i++)
                {
                    for (int j = 0; j < _puzzleSize; j++)
                    {
                        if (_puzzle[i, j] == 0)
                        {
                            badPuzzle = true;
                        }
                    }
                }
            } while (badPuzzle == true);

            
        }

        public PuzzleNine(int typeNumForEmpty)
        {
            _puzzle = new int[_puzzleSize, _puzzleSize];
        }

        public PuzzleNine CopyPuzzle()
        {
            // just need an empty puzzle
            PuzzleNine aClone = new PuzzleNine(1);

            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    aClone._puzzle[i, j] = _puzzle[i, j];
                }
            }
            return aClone;

        }

        public void UpdateCoordinates()
        {
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    if (_puzzle[i, j] != 0)
                    {
                        _coordinateTable[i, j] = false;
                    }
                }
            }
        }

        public void ResetCoordinates()
        {
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    _coordinateTable[i, j] = true;
                }
            }        

        }

        private void makeNewPuzzle()
        {
            _puzzle = new int[_puzzleSize, _puzzleSize];

            // make an empty table
            //_puzzle = emptyTable();

            //reset _coordinates taken table
            resetTruthTable(_coordinateTable);

            // get inital starting point
            int value;

            //this is a holding place for the values that still need to be added to the puzzle
            List<int> values = new List<int>();
            //loop until puzzle has all these values
            for (int i = 1; i < _puzzleSize + 1; i++)
            {
                values.Add(i);
            }

            for (int i = 0; i < _puzzleSize; i++)
            {
                value = values[0];

                if (values.Count != 0)
                {
                    value = values[_random.Next(0, values.Count - 1)];
                }

                // remove the first one
                values.Remove(value);

                // fill in all the places the value should go in the puzzle
                addOneValueToPuzzle(value);

                // update hasValueAlreadyArray
                UpdateCoordinates();

                // use updated coordinates taken to set the test table

            }

            // add every remaining value to puzzle

            // pick a new value

        }

        private void addOneValueToPuzzle(int value)
        {
            //create a table that holds where a value can and can't go
            //each value needs a new test table
            bool[,] testTable = truthTable();

            // update available places
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    if (_coordinateTable[i, j] == false)
                    {
                        testTable[i, j] = false;
                    }
                }
            }

            while (true)
            {
                // create a list of all available positions
                List<int[]> availablePositions = new List<int[]>();

                availablePositions = findAvailablePositions(testTable, availablePositions);

                if (availablePositions.Count == 0)
                {
                    break;
                }

                // grab a random position, row and column
                int[] addValueHere = pickALocation(availablePositions);

                // put the value in this position
                int row = addValueHere[0];
                int column = addValueHere[1];

                //add to puzzle
                populateValue(row, column, value);

                testTable = updateTestTable(testTable, row, column);
            }

        }

        private void populateValue(int row, int column, int value)
        {
            _puzzle[row, column] = value;
        }

        private List<int[]> findAvailablePositions(bool[,] testTable, List<int[]> availablePositions)
        {
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    //keep an array to hold the position
                    int[] position = new int[2];
                    //using the test table as a reference, make a list of all the available positions
                    if (testTable[i, j])
                    {
                        //row
                        position[0] = i;
                        //column
                        position[1] = j;

                        availablePositions.Add(position);
                    }
                }
            }

            return availablePositions;

            //at this point, use get a random index position and place the value
        }

        public List<int[]> FindTrueCoordinateTable()
        {
            List<int[]> availablePositions = new List<int[]>();

            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    //keep an array to hold the position                    
                    int[] position = new int[2];

                    //using the test table as a reference, make a list of all the available positions
                    if (_coordinateTable[i, j])
                    {
                        //row
                        position[0] = i;
                        //column
                        position[1] = j;

                        availablePositions.Add(position);
                    }
                }
            }

            return availablePositions;

            //at this point, use get a random index position and place the value
        }


        private bool[,] updateTestTable(bool[,] truthTable, int row, int column)
        {
            // check the updated coordinates and take those values out
            // start with the same position, logically fill in the rest
            truthTable[row, column] = false;

            //can't have this value in every row at the column index
            for (int rows = 0; rows < _puzzleSize; rows++)
            {
                truthTable[rows, column] = false;
            }

            //can't have this value in the row
            for (int cols = 0; cols < _puzzleSize; cols++)
            {
                truthTable[row, cols] = false;
            }

            // can't have the value in whatever box it is in
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

            if (boxTopLeft)
            {
                for (int rows = 0; rows < 3; rows++)
                {
                    for (int cols = 0; cols < 3; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            else if (boxTopMid)
            {
                for (int rows = 0; rows < 3; rows++)
                {
                    for (int cols = 3; cols < 6; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            else if (boxTopRight)
            {
                for (int rows = 0; rows < 3; rows++)
                {
                    for (int cols = 6; cols < 9; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            // seccond row of boxes
            else if (boxMidLeft)
            {
                for (int rows = 3; rows < 6; rows++)
                {
                    for (int cols = 0; cols < 3; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            else if (boxMidMid)
            {
                for (int rows = 3; rows < 6; rows++)
                {
                    for (int cols = 3; cols < 6; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            else if (boxMidRight)
            {
                for (int rows = 3; rows < 6; rows++)
                {
                    for (int cols = 6; cols < 9; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            // row 3 of boxes
            else if (boxBottomLeft)
            {
                for (int rows = 6; rows < 9; rows++)
                {
                    for (int cols = 0; cols < 3; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            else if (boxBottomMid)
            {
                for (int rows = 6; rows < 9; rows++)
                {
                    for (int cols = 3; cols < 6; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }
            else if (boxBottomRight)
            {
                for (int rows = 6; rows < 9; rows++)
                {
                    for (int cols = 6; cols < 9; cols++)
                    {
                        truthTable[rows, cols] = false;
                    }
                }
            }

            return truthTable;

        }

        private bool[,] truthTable()
        {
            bool[,] testTable = new bool[_puzzleSize, _puzzleSize];

            testTable = resetTruthTable(testTable);

            return testTable;
        }

        private bool[,] resetTruthTable(bool[,] testTable)
        {
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    testTable[i, j] = true;
                }
            }
            return testTable;
        }

        private int[] pickALocation(List<int[]> availablePositions)
        {

            int randomIndex = 0;

            if (availablePositions.Count != 1)
            {
                randomIndex = _random.Next(0, availablePositions.Count - 1);
            }
            return availablePositions[randomIndex];
        }
    }
}
