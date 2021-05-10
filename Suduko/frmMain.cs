using ControlLogic;
using PuzzleEntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suduko
{
    public partial class frmMain : Form
    {
        private SetupPuzzle _setup = new ControlLogic.SetupPuzzle(0);
        public PuzzleNine _userAnswers { get; private set; }
        private int _puzzleSize = 9;
        private int _difficultySelection;
        public Enum DifficultyRating;
        private bool puzzleLoadFlag = false;
        private bool puzzleReady = false;
        private Canidates _pencilMarks = new Canidates();

        public int DifficultlySelection
        {
            get { return _difficultySelection; }
            set { _difficultySelection = value; }
        }

        public frmMain()
        {
            InitializeComponent();
        }

        public SetupPuzzle Setup
        {
            get { return _setup; }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // load last saved game
            try
            {
                loadGameToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error loading the previous save.", "Load Error",
                //    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnNewGame_Click(sender, e);
            }

            addEventsToTextBoxes(this.Controls);

        }

        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (puzzleLoadFlag)
            {
                if (MessageBox.Show("This will overwrite any unsaved data.\n\nContinue?",
                    "Reload Data.",
                        MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            puzzleReady = false;

            UIHelpers.CloseOpenWindows();

            UIHelpers.ClearControls(this.Controls);
            _pencilMarks = new Canidates();

            try
            {
                _userAnswers = _setup.LoadSavedPuzzle();
                puzzleLoadFlag = true;
                _pencilMarks.LoadPencilMarks();
                fillSetupPuzzle();

                _userAnswers.ResetCoordinates();
                fillUserAnswers();
                combineTruthSetupAndUsers();

            }
            catch (Exception ex)
            {                
                MessageBox.Show("Can't load a previously saved puzzle.\nThis may be because it is the first time loading."
                    + ex.Message + "\n\n" + ex.InnerException.Message);
                btnNewGame_Click(sender, e);
            }

            puzzleReady = true;

            toolStrip.Text = "Good luck and have fun!";
        }

        private void btnShowSolution_Click(object sender, EventArgs e)
        {
            frmPuzzleSolution puzzleSolution = new frmPuzzleSolution(Setup);

            puzzleSolution.Show();
        }

        private void featuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFeatures features = new frmFeatures();
            features.Show();

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {

            var result = MessageBox.Show("Are you sure you would like to start a new game?",
                "New Game", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);            

            if (result == DialogResult.OK)
            {
                UIHelpers.CloseOpenWindows();

                UIHelpers.ClearControls(this.Controls);
                _pencilMarks = new Canidates();
                _setup = new ControlLogic.SetupPuzzle(_difficultySelection);
                _setup.UpdateCanidates();

                puzzleReady = false;
                _difficultySelection = cmboxDifficulty.SelectedIndex;
                _userAnswers = new PuzzleNine(1);

                fillSetupPuzzle();

                puzzleReady = true;

                _userAnswers.ResetCoordinates();
                combineTruthSetupAndUsers();

                toolStrip.Text = "Good luck and have fun!";
            }
        }

        private void menuNewGame_Click(object sender, EventArgs e)
        {
            btnNewGame_Click(sender, e);
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmboxDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            _difficultySelection = cmboxDifficulty.SelectedIndex;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Suduko Puzzle\nCopyright: Derrick Nagy\nCreated by Derrick Nagy for .NET 1 at Kirkwood Community College\nSpring 2021",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (puzzleLoadFlag)
            {
                if (MessageBox.Show("This will overwrite any saved data.\n\nContinue?",
                    "Reload Data", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            // going to set it up that there is always a puzzle that loads so
            // there is always something to save.

            try
            {
                // wrap up puzzle information into a list
                List<int[,]> puzzleToSave = new List<int[,]>();
                puzzleToSave.Add(_setup.puzzleSolution._puzzle);
                puzzleToSave.Add(_setup.setupPuzzle._puzzle);
                puzzleToSave.Add(_userAnswers._puzzle);


                


                SaveLoad save = new SaveLoad();
                
                ///pencilMarks.CanidateList = _setup.CanidateList.CanidateList;

                if (save.SaveSolutionAndSetup(puzzleToSave) &&
                    _pencilMarks.SavePencilMarks(_pencilMarks.CanidateList)
                    )
                {
                    MessageBox.Show("Puzzle was saved");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException);
            }
        }

        private void isSolved()
        {
            // see if the puzzle is complete, and if so, correct
            // if coord value = 0, it is true

            bool puzzleComplete = isComplete();


            if (puzzleComplete == true)
            {
                //test the rows

                bool allRowsCorrect = false;
                bool allColumnsCorrect = false;

                allRowsCorrect = rowsAreComplete();
                allColumnsCorrect = columnsAreComplete();


                if (allRowsCorrect && allColumnsCorrect)
                {
                    // the puzzle is correct
                    toolStrip.Text = "You win!";
                    MessageBox.Show("Congratulations, you correctly solved the puzzle!", "Winner!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    toolStrip.Text = "There are one or more incorrect answers.";
                }
            }
        }

        private bool columnsAreComplete()
        {
            bool allColsCorrect = false;

            // see if it is correct
            int correctCols = 0;

            //45 is a correct value

            for (int col = 0; col < _puzzleSize; col++)
            {
                int colTotal = 0;

                for (int row = 0; row < _puzzleSize; row++)
                {
                    colTotal += _userAnswers._puzzle[row, col];
                    colTotal += _setup.setupPuzzle._puzzle[row, col];
                }

                if (colTotal == 45)
                {
                    //the row is correct
                    correctCols++;
                }
            }

            if (correctCols == 9)
            {
                //all the rows are correct
                allColsCorrect = true;
            }
            return allColsCorrect;

        }

        private bool rowsAreComplete()
        {
            bool allRowsCorrect = false;

            // see if it is correct
            int correctRows = 0;

            //45 is a correct value
            for (int row = 0; row < _puzzleSize; row++)
            {
                int rowTotal = 0;
                for (int col = 0; col < _puzzleSize; col++)
                {
                    rowTotal += _userAnswers._puzzle[row, col];
                    rowTotal += _setup.setupPuzzle._puzzle[row, col];
                }

                if (rowTotal == 45)
                {
                    //the row is correct
                    correctRows++;
                }
            }

            if (correctRows == 9)
            {
                //all the rows are correct
                allRowsCorrect = true;
            }
            return allRowsCorrect;
        }

        private bool isComplete()
        {
            //update the truth table for the userAnswers object
            _userAnswers.UpdateCoordinates();

            bool puzzleComplete = true;

            for (int row = 0; row < _puzzleSize; row++)
            {
                for (int col = 0; col < _puzzleSize; col++)
                {
                    if (_userAnswers.CoordinateTable[row, col] == true)
                    {
                        // needs to be filled out
                        puzzleComplete = false;
                        break;
                    }
                }
            }
            return puzzleComplete;
        }

        private void combineTruthSetupAndUsers()
        {
            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    if (_setup.setupPuzzle.CoordinateTable[i, j] == false)
                    {
                        _userAnswers.CoordinateTable[i, j] = false;
                    }
                }
            }
        }

        private List<int[]> availableTxtBoxes(int[] currentCoords)
        {
            // each txt box needs a place in its row and colum to go next
            // i want this to return: xy coords above, below, left, right of the parameter

            // 1. know where it is
            int x = currentCoords[0];
            int y = currentCoords[1];
            

            // available boxes in the row (including self)
            //List<int[]> horizontalPositions = new List<int[]>();
            List<int[]> verticalPositions = new List<int[]>();
            //instead of just the row, get the whole puzzle
            
            for (int i = 0; i < _puzzleSize; i++)
            {
                if (_setup.setupPuzzle._puzzle[i, y] == 0)
                {
                    int[] xycoords = { i, y };
                    verticalPositions.Add(xycoords);
                }
            }

            int vpIndex = 0;
            for (int i = 0; i < verticalPositions.Count; i++)
            {
                int[] xyCoords = verticalPositions[i];

                if (xyCoords[0] == x && xyCoords[1] == y)
                {
                    vpIndex = i;
                }
            }

            int[] right, left, above, below = new int[2];

            if (vpIndex == 0)
            {
                //top
                above = currentCoords;

                if (verticalPositions.Count == 1)
                {
                    //if there is only one option
                    below = currentCoords;
                }
                else
                {
                    below = verticalPositions[vpIndex + 1];
                }                
            }
            else if (vpIndex == verticalPositions.Count - 1)
            {
                above = verticalPositions[vpIndex - 1];
                below = currentCoords;
            }
            else
            {
                above = verticalPositions[vpIndex - 1];
                below = verticalPositions[vpIndex + 1];
            }

            List<int[]> availableTextBoxes =  new List<int[]>();

            //so that arrows move to the next row when appropriate
            _setup.CanidateList = new Canidates();
            _setup.UpdateCanidates();
            int[] right2 = _setup.CanidateList.CoordsRight(currentCoords);
            int[] left2 = _setup.CanidateList.CoordsLeft(currentCoords);

            availableTextBoxes.Add(right2);
            availableTextBoxes.Add(left2);
            availableTextBoxes.Add(above);
            availableTextBoxes.Add(below);
            availableTextBoxes.Add(currentCoords);

            return availableTextBoxes;
        } 

        private int[] shiftFocus(List<int[]> availableTextBoxes, Enum direction)
        {
            int[] right, left, above, below, origin = new int[2];

            if (direction.Equals(Direction.right))
            {
                right = availableTextBoxes[0];
                return right;
            } else if (direction.Equals(Direction.left))
            {
                left = availableTextBoxes[1];
                return left;
            }
            else if (direction.Equals(Direction.above))
            {
                above = availableTextBoxes[2];
                return above;
            }
            else if (direction.Equals(Direction.below))
            {
                below = availableTextBoxes[3];
                return below;
            }
            else
            {
                origin = availableTextBoxes[4];
                return origin;
            }
        }

        private List<Control> getTextBoxes()
        {            
            List<Control> textBoxControls = new List<Control>();

            var grpBxControls = groupBox1.Controls;

            foreach (Control control in grpBxControls)
            {
                if (control is TextBox)
                {
                    textBoxControls.Add(control);
                }
            }
            textBoxControls.Reverse();

            return textBoxControls;
        }

        private Control[,] getEnabledTextBoxes(List<Control> textBoxControls)
        {
            Control[,] textBoxes = new Control[_puzzleSize, _puzzleSize];

            int elements = 0;

            for (int i = 0; i < _puzzleSize; i++)
            {
                for (int j = 0; j < _puzzleSize; j++)
                {
                    textBoxes[i,j] = textBoxControls[elements];
                    elements++;
                }
            }
            return textBoxes;
        }

        private void arrowPointer(int[] boxCoords, KeyEventArgs e)
        {
            _setup.UpdateSetupCoordiateTable();
            

            //list of places I can go to from here 
            List<int[]> options = availableTxtBoxes(boxCoords);

            //find the coresponding coords of the control
            Control[,] controlCoords = getEnabledTextBoxes(getTextBoxes());

            // right, left, above, below

            if (e.KeyCode == Keys.Right)
            {
                // need the coords of the first available below it
                int[] goHere = shiftFocus(options, Direction.right);

                // then shift focus to it
                controlCoords[goHere[0], goHere[1]].Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                // need the coords of the first available below it
                int[] goHere = shiftFocus(options, Direction.left);

                // then shift focus to it
                controlCoords[goHere[0], goHere[1]].Focus();

            }
            else if (e.KeyCode == Keys.Up)
            {
                // need the coords of the first available below it
                int[] goHere = shiftFocus(options, Direction.above);

                // then shift focus to it
                controlCoords[goHere[0], goHere[1]].Focus();

            }
            else if (e.KeyCode == Keys.Down)
            {
                // need the coords of the first available below it
                int[] goHere = shiftFocus(options, Direction.below);

                // then shift focus to it
                controlCoords[goHere[0], goHere[1]].Focus();
            }
        }

        private void textChanged(object sender, EventArgs e)
        {           
            if (puzzleReady)
            {                
                var ctrl = (Control)sender;
                string contentString = ctrl.Text.TrimStart();
                List<char> contentList = new List<char>();

                // get the location of the txtbox from it's tag property
                int[] coords = UIHelpers.GetTxtBoxCoords(ctrl);

                //break up the string into a char list
                for (int i = 0; i < contentString.Length; i++)
                {
                    // if the content is between 1 and 9 or a space or a comma
                    if ((contentString[i] >= '1' && contentString[i] <= '9')
                        || contentString[i] == ' '
                        || contentString[i] == ','
                        )
                        contentList.Add(contentString[i]);
                }

                if (contentList.Count > 1)
                {
                    string textBoxString = "";
                    List<int> markNumber = new List<int>(); 
                    foreach (char content in contentList)
                    {
                        textBoxString += content;
                        if (content >= '1' && content <= '9')
                        {
                            markNumber.Add((int)Char.GetNumericValue(content));
                        }
                    }

                    ctrl.Text = textBoxString;
                    UIHelpers.PencilMarkFormat(ctrl);

                    //before adding, remove any old answers
                    int index = _pencilMarks.IndexOfCanidate(coords);
                    if (index != -1)
                    {
                        _pencilMarks.RemoveCanidateByCoords(coords);
                    }

                    //add to pencilMarks for saving
                    SingleCanidate pencilMark = new SingleCanidate(coords, markNumber);

                    //pencilMark.Coordinates = coords;
                    //pencilMark.PossibleAnswers = markNumber;

                    _pencilMarks.AddCanidate(pencilMark);

                }
                else if (contentList.Count == 1)
                {
                    //ctrl.Font = new Font(ctrl.Font.FontFamily, 36);
                    UIHelpers.UserAnswersFormat(ctrl);
                    int num = (int)Char.GetNumericValue(contentList[0]);

                    //put text input into the user answers at the appropriiate coordinate
                    _userAnswers._puzzle[coords[0], coords[1]] = num;
                    //ctrl.BackColor = Color.LightBlue;
                    ctrl.Text = contentList[0].ToString();
                    isSolved();
                }
                else if (ctrl.Text.Equals(""))
                {
                    UIHelpers.UserAnswersFormat(ctrl);

                    //ctrl.BackColor = Color.LightBlue;
                    //ctrl.Font = new Font(ctrl.Font.FontFamily, 36);
                    _userAnswers._puzzle[coords[0], coords[1]] = 0;
                }
                else if (ctrl.Text.Equals(" "))
                {
                    //ctrl.BackColor = Color.LightBlue;
                    //ctrl.Font = new Font(ctrl.Font.FontFamily, 36);
                    UIHelpers.UserAnswersFormat(ctrl);
                    ctrl.Text = "";                   
                }
            }
        }

        private void fillSetupPuzzle()
        {
            List<Control> txtBoxList = getTextBoxes();

            foreach (Control control in txtBoxList)
            {
                //get coords of control
                int[] coords = UIHelpers.GetTxtBoxCoords(control);
                int x = coords[0];
                int y = coords[1];

                if (_setup.setupPuzzle._puzzle[x,y] != 0)
                {
                    control.Text = _setup.setupPuzzle._puzzle[x, y].ToString();
                    control.Enabled = false;
                }
                else
                {
                    control.BackColor = Color.LightBlue;
                }
            }
        }

        private void fillUserAnswers()
        {
            List<Control> txtBoxList = getTextBoxes();

            foreach (Control control in txtBoxList)
            {
                //get coords of control
                int[] coords = UIHelpers.GetTxtBoxCoords(control);
                int x = coords[0];
                int y = coords[1];

                if (_userAnswers._puzzle[x,y] != 0)
                {
                    control.Text = _userAnswers._puzzle[x, y].ToString();
                }

                //find a pencil mark that has these coords if exists
                int index = _pencilMarks.IndexOfCanidate(coords);

                if (index != -1)
                {
                    //found it
                    string marks = "";

                    foreach (int num in _pencilMarks.CanidateList[index].PossibleAnswers)
                    {
                        marks += num.ToString() + " ";
                    }

                    control.Text = marks.TrimEnd();

                    UIHelpers.PencilMarkFormat(control);
                }
            }
        }

        private void addEventsToTextBoxes(Control.ControlCollection controlCollection)
        {
            foreach (Control control in controlCollection)
            {
                if (control is TextBox)
                {
                    control.DoubleClick += Control_DoubleClick;
                    control.Leave += Control_Leave;
                    control.KeyUp += Control_KeyUp;
                    control.TextChanged += Control_TextChanged;
                }
                else
                {
                    addEventsToTextBoxes(control.Controls);
                }
            }
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            textChanged(sender, e);
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            var control = (Control)sender;
            int[] boxCoords = UIHelpers.GetTxtBoxCoords(control);
            arrowPointer(boxCoords, e);
        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {
            UIHelpers.DoubleClickColorChange(sender);
        }

        public void Control_Leave(object sender, EventArgs e)
        {
            toolStrip.Text = "";
        }

    }
}
