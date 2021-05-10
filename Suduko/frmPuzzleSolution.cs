using ControlLogic;
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
    public partial class frmPuzzleSolution : Form
    {
        private SetupPuzzle _setup;

        public frmPuzzleSolution(SetupPuzzle setup)
        {

            _setup = setup;

            InitializeComponent();

        }

        private void frmPuzzleSolution_Load(object sender, EventArgs e)
        {


            fillRowOne();
            fillRowTwo();
            fillRowThree();
            fillRowFour();
            fillRowFive();
            fillRowSix();
            fillRowSeven();
            fillRowEight();
            fillRowNine();

        }

        private void fillRowOne()
        {
            if (_setup.setupPuzzle._puzzle[0, 0] == 0)
            {
                txtR0C0.BackColor = Color.LightBlue;
            }
            txtR0C0.Text = _setup.puzzleSolution._puzzle[0, 0].ToString();

            if (_setup.setupPuzzle._puzzle[0, 1] == 0)
            {
                txtR0C1.BackColor = Color.LightBlue;
            }
            txtR0C1.Text = _setup.puzzleSolution._puzzle[0, 1].ToString();


            if (_setup.setupPuzzle._puzzle[0, 2] == 0)
            {
                txtR0C2.BackColor = Color.LightBlue;
            }
            txtR0C2.Text = _setup.puzzleSolution._puzzle[0, 2].ToString();


            if (_setup.setupPuzzle._puzzle[0, 3] == 0)
            {
                txtR0C3.BackColor = Color.LightBlue;
            }
            txtR0C3.Text = _setup.puzzleSolution._puzzle[0, 3].ToString();

            if (_setup.setupPuzzle._puzzle[0, 4] == 0)
            {
                txtR0C4.BackColor = Color.LightBlue;
            }
            txtR0C4.Text = _setup.puzzleSolution._puzzle[0, 4].ToString();


            if (_setup.setupPuzzle._puzzle[0, 5] == 0)
            {
                txtR0C5.BackColor = Color.LightBlue;
            }
            txtR0C5.Text = _setup.puzzleSolution._puzzle[0, 5].ToString();

            if (_setup.setupPuzzle._puzzle[0, 6] == 0)
            {
                txtR0C6.BackColor = Color.LightBlue;
            }
            txtR0C6.Text = _setup.puzzleSolution._puzzle[0, 6].ToString();


            if (_setup.setupPuzzle._puzzle[0, 7] == 0)
            {
                txtR0C7.BackColor = Color.LightBlue;
            }
            txtR0C7.Text = _setup.puzzleSolution._puzzle[0, 7].ToString();

            if (_setup.setupPuzzle._puzzle[0, 8] == 0)
            {
                txtR0C8.BackColor = Color.LightBlue;
            }
            txtR0C8.Text = _setup.puzzleSolution._puzzle[0, 8].ToString();

        }

        private void fillRowTwo()
        {
            if (_setup.setupPuzzle._puzzle[1, 0] == 0)
            {
                txtR1C0.BackColor = Color.LightBlue;
            }
            txtR1C0.Text = _setup.puzzleSolution._puzzle[1, 0].ToString();

            if (_setup.setupPuzzle._puzzle[1, 1] == 0)
            {
                txtR1C1.BackColor = Color.LightBlue;
            }
            txtR1C1.Text = _setup.puzzleSolution._puzzle[1, 1].ToString();


            if (_setup.setupPuzzle._puzzle[1, 2] == 0)
            {
                txtR1C2.BackColor = Color.LightBlue;
            }
            txtR1C2.Text = _setup.puzzleSolution._puzzle[1, 2].ToString();


            if (_setup.setupPuzzle._puzzle[1, 3] == 0)
            {
                txtR1C3.BackColor = Color.LightBlue;
            }
            txtR1C3.Text = _setup.puzzleSolution._puzzle[1, 3].ToString();

            if (_setup.setupPuzzle._puzzle[1, 4] == 0)
            {
                txtR1C4.BackColor = Color.LightBlue;
            }
            txtR1C4.Text = _setup.puzzleSolution._puzzle[1, 4].ToString();


            if (_setup.setupPuzzle._puzzle[1, 5] == 0)
            {
                txtR1C5.BackColor = Color.LightBlue;
            }
            txtR1C5.Text = _setup.puzzleSolution._puzzle[1, 5].ToString();

            if (_setup.setupPuzzle._puzzle[1, 6] == 0)
            {
                txtR1C6.BackColor = Color.LightBlue;
            }
            txtR1C6.Text = _setup.puzzleSolution._puzzle[1, 6].ToString();


            if (_setup.setupPuzzle._puzzle[1, 7] == 0)
            {
                txtR1C7.BackColor = Color.LightBlue;
            }
            txtR1C7.Text = _setup.puzzleSolution._puzzle[1, 7].ToString();

            if (_setup.setupPuzzle._puzzle[1, 8] == 0)
            {
                txtR1C8.BackColor = Color.LightBlue;
            }
            txtR1C8.Text = _setup.puzzleSolution._puzzle[1, 8].ToString();

        }

        private void fillRowThree()
        {
            if (_setup.setupPuzzle._puzzle[2, 0] == 0)
            {
                txtR2C0.BackColor = Color.LightBlue;
            }
            txtR2C0.Text = _setup.puzzleSolution._puzzle[2, 0].ToString();

            if (_setup.setupPuzzle._puzzle[2, 1] == 0)
            {
                txtR2C1.BackColor = Color.LightBlue;
            }
            txtR2C1.Text = _setup.puzzleSolution._puzzle[2, 1].ToString();


            if (_setup.setupPuzzle._puzzle[2, 2] == 0)
            {
                txtR2C2.BackColor = Color.LightBlue;
            }
            txtR2C2.Text = _setup.puzzleSolution._puzzle[2, 2].ToString();


            if (_setup.setupPuzzle._puzzle[2, 3] == 0)
            {
                txtR2C3.BackColor = Color.LightBlue;
            }
            txtR2C3.Text = _setup.puzzleSolution._puzzle[2, 3].ToString();

            if (_setup.setupPuzzle._puzzle[2, 4] == 0)
            {
                txtR2C4.BackColor = Color.LightBlue;
            }
            txtR2C4.Text = _setup.puzzleSolution._puzzle[2, 4].ToString();


            if (_setup.setupPuzzle._puzzle[2, 5] == 0)
            {
                txtR2C5.BackColor = Color.LightBlue;
            }
            txtR2C5.Text = _setup.puzzleSolution._puzzle[2, 5].ToString();

            if (_setup.setupPuzzle._puzzle[2, 6] == 0)
            {
                txtR2C6.BackColor = Color.LightBlue;
            }
            txtR2C6.Text = _setup.puzzleSolution._puzzle[2, 6].ToString();


            if (_setup.setupPuzzle._puzzle[2, 7] == 0)
            {
                txtR2C7.BackColor = Color.LightBlue;
            }
            txtR2C7.Text = _setup.puzzleSolution._puzzle[2, 7].ToString();

            if (_setup.setupPuzzle._puzzle[2, 8] == 0)
            {
                txtR2C8.BackColor = Color.LightBlue;
            }
            txtR2C8.Text = _setup.puzzleSolution._puzzle[2, 8].ToString();

        }

        private void fillRowFour()
        {
            if (_setup.setupPuzzle._puzzle[3, 0] == 0)
            {
                txtR3C0.BackColor = Color.LightBlue;
            }
            txtR3C0.Text = _setup.puzzleSolution._puzzle[3, 0].ToString();

            if (_setup.setupPuzzle._puzzle[3, 1] == 0)
            {
                txtR3C1.BackColor = Color.LightBlue;
            }
            txtR3C1.Text = _setup.puzzleSolution._puzzle[3, 1].ToString();


            if (_setup.setupPuzzle._puzzle[3, 2] == 0)
            {
                txtR3C2.BackColor = Color.LightBlue;
            }
            txtR3C2.Text = _setup.puzzleSolution._puzzle[3, 2].ToString();


            if (_setup.setupPuzzle._puzzle[3, 3] == 0)
            {
                txtR3C3.BackColor = Color.LightBlue;
            }
            txtR3C3.Text = _setup.puzzleSolution._puzzle[3, 3].ToString();

            if (_setup.setupPuzzle._puzzle[3, 4] == 0)
            {
                txtR3C4.BackColor = Color.LightBlue;
            }
            txtR3C4.Text = _setup.puzzleSolution._puzzle[3, 4].ToString();


            if (_setup.setupPuzzle._puzzle[3, 5] == 0)
            {
                txtR3C5.BackColor = Color.LightBlue;
            }
            txtR3C5.Text = _setup.puzzleSolution._puzzle[3, 5].ToString();

            if (_setup.setupPuzzle._puzzle[3, 6] == 0)
            {
                txtR3C6.BackColor = Color.LightBlue;
            }
            txtR3C6.Text = _setup.puzzleSolution._puzzle[3, 6].ToString();


            if (_setup.setupPuzzle._puzzle[3, 7] == 0)
            {
                txtR3C7.BackColor = Color.LightBlue;
            }
            txtR3C7.Text = _setup.puzzleSolution._puzzle[3, 7].ToString();

            if (_setup.setupPuzzle._puzzle[3, 8] == 0)
            {
                txtR3C8.BackColor = Color.LightBlue;
            }
            txtR3C8.Text = _setup.puzzleSolution._puzzle[3, 8].ToString();

        }

        private void fillRowFive()
        {
            if (_setup.setupPuzzle._puzzle[4, 0] == 0)
            {
                txtR4C0.BackColor = Color.LightBlue;
            }
            txtR4C0.Text = _setup.puzzleSolution._puzzle[4, 0].ToString();

            if (_setup.setupPuzzle._puzzle[4, 1] == 0)
            {
                txtR4C1.BackColor = Color.LightBlue;
            }
            txtR4C1.Text = _setup.puzzleSolution._puzzle[4, 1].ToString();


            if (_setup.setupPuzzle._puzzle[4, 2] == 0)
            {
                txtR4C2.BackColor = Color.LightBlue;
            }
            txtR4C2.Text = _setup.puzzleSolution._puzzle[4, 2].ToString();


            if (_setup.setupPuzzle._puzzle[4, 3] == 0)
            {
                txtR4C3.BackColor = Color.LightBlue;
            }
            txtR4C3.Text = _setup.puzzleSolution._puzzle[4, 3].ToString();

            if (_setup.setupPuzzle._puzzle[4, 4] == 0)
            {
                txtR4C4.BackColor = Color.LightBlue;
            }
            txtR4C4.Text = _setup.puzzleSolution._puzzle[4, 4].ToString();


            if (_setup.setupPuzzle._puzzle[4, 5] == 0)
            {
                txtR4C5.BackColor = Color.LightBlue;
            }
            txtR4C5.Text = _setup.puzzleSolution._puzzle[4, 5].ToString();

            if (_setup.setupPuzzle._puzzle[4, 6] == 0)
            {
                txtR4C6.BackColor = Color.LightBlue;
            }
            txtR4C6.Text = _setup.puzzleSolution._puzzle[4, 6].ToString();


            if (_setup.setupPuzzle._puzzle[4, 7] == 0)
            {
                txtR4C7.BackColor = Color.LightBlue;
            }
            txtR4C7.Text = _setup.puzzleSolution._puzzle[4, 7].ToString();

            if (_setup.setupPuzzle._puzzle[4, 8] == 0)
            {
                txtR4C8.BackColor = Color.LightBlue;
            }
            txtR4C8.Text = _setup.puzzleSolution._puzzle[4, 8].ToString();

        }

        private void fillRowSix()
        {
            if (_setup.setupPuzzle._puzzle[5, 0] == 0)
            {
                txtR5C0.BackColor = Color.LightBlue;
            }
            txtR5C0.Text = _setup.puzzleSolution._puzzle[5, 0].ToString();

            if (_setup.setupPuzzle._puzzle[5, 1] == 0)
            {
                txtR5C1.BackColor = Color.LightBlue;
            }
            txtR5C1.Text = _setup.puzzleSolution._puzzle[5, 1].ToString();


            if (_setup.setupPuzzle._puzzle[5, 2] == 0)
            {
                txtR5C2.BackColor = Color.LightBlue;
            }
            txtR5C2.Text = _setup.puzzleSolution._puzzle[5, 2].ToString();


            if (_setup.setupPuzzle._puzzle[5, 3] == 0)
            {
                txtR5C3.BackColor = Color.LightBlue;
            }
            txtR5C3.Text = _setup.puzzleSolution._puzzle[5, 3].ToString();

            if (_setup.setupPuzzle._puzzle[5, 4] == 0)
            {
                txtR5C4.BackColor = Color.LightBlue;
            }
            txtR5C4.Text = _setup.puzzleSolution._puzzle[5, 4].ToString();


            if (_setup.setupPuzzle._puzzle[5, 5] == 0)
            {
                txtR5C5.BackColor = Color.LightBlue;
            }
            txtR5C5.Text = _setup.puzzleSolution._puzzle[5, 5].ToString();

            if (_setup.setupPuzzle._puzzle[5, 6] == 0)
            {
                txtR5C6.BackColor = Color.LightBlue;
            }
            txtR5C6.Text = _setup.puzzleSolution._puzzle[5, 6].ToString();


            if (_setup.setupPuzzle._puzzle[5, 7] == 0)
            {
                txtR5C7.BackColor = Color.LightBlue;
            }
            txtR5C7.Text = _setup.puzzleSolution._puzzle[5, 7].ToString();

            if (_setup.setupPuzzle._puzzle[5, 8] == 0)
            {
                txtR5C8.BackColor = Color.LightBlue;
            }
            txtR5C8.Text = _setup.puzzleSolution._puzzle[5, 8].ToString();

        }

        private void fillRowSeven()
        {
            if (_setup.setupPuzzle._puzzle[6, 0] == 0)
            {
                txtR6C0.BackColor = Color.LightBlue;
            }
            txtR6C0.Text = _setup.puzzleSolution._puzzle[6, 0].ToString();

            if (_setup.setupPuzzle._puzzle[6, 1] == 0)
            {
                txtR6C1.BackColor = Color.LightBlue;
            }
            txtR6C1.Text = _setup.puzzleSolution._puzzle[6, 1].ToString();


            if (_setup.setupPuzzle._puzzle[6, 2] == 0)
            {
                txtR6C2.BackColor = Color.LightBlue;
            }
            txtR6C2.Text = _setup.puzzleSolution._puzzle[6, 2].ToString();


            if (_setup.setupPuzzle._puzzle[6, 3] == 0)
            {
                txtR6C3.BackColor = Color.LightBlue;
            }
            txtR6C3.Text = _setup.puzzleSolution._puzzle[6, 3].ToString();

            if (_setup.setupPuzzle._puzzle[6, 4] == 0)
            {
                txtR6C4.BackColor = Color.LightBlue;
            }
            txtR6C4.Text = _setup.puzzleSolution._puzzle[6, 4].ToString();


            if (_setup.setupPuzzle._puzzle[6, 5] == 0)
            {
                txtR6C5.BackColor = Color.LightBlue;
            }
            txtR6C5.Text = _setup.puzzleSolution._puzzle[6, 5].ToString();

            if (_setup.setupPuzzle._puzzle[6, 6] == 0)
            {
                txtR6C6.BackColor = Color.LightBlue;
            }
            txtR6C6.Text = _setup.puzzleSolution._puzzle[6, 6].ToString();


            if (_setup.setupPuzzle._puzzle[6, 7] == 0)
            {
                txtR6C7.BackColor = Color.LightBlue;
            }
            txtR6C7.Text = _setup.puzzleSolution._puzzle[6, 7].ToString();

            if (_setup.setupPuzzle._puzzle[6, 8] == 0)
            {
                txtR6C8.BackColor = Color.LightBlue;
            }
            txtR6C8.Text = _setup.puzzleSolution._puzzle[6, 8].ToString();

        }

        private void fillRowEight()
        {
            if (_setup.setupPuzzle._puzzle[7, 0] == 0)
            {
                txtR7C0.BackColor = Color.LightBlue;
            }
            txtR7C0.Text = _setup.puzzleSolution._puzzle[7, 0].ToString();

            if (_setup.setupPuzzle._puzzle[7, 1] == 0)
            {
                txtR7C1.BackColor = Color.LightBlue;
            }
            txtR7C1.Text = _setup.puzzleSolution._puzzle[7, 1].ToString();


            if (_setup.setupPuzzle._puzzle[7, 2] == 0)
            {
                txtR7C2.BackColor = Color.LightBlue;
            }
            txtR7C2.Text = _setup.puzzleSolution._puzzle[7, 2].ToString();


            if (_setup.setupPuzzle._puzzle[7, 3] == 0)
            {
                txtR7C3.BackColor = Color.LightBlue;
            }
            txtR7C3.Text = _setup.puzzleSolution._puzzle[7, 3].ToString();

            if (_setup.setupPuzzle._puzzle[7, 4] == 0)
            {
                txtR7C4.BackColor = Color.LightBlue;
            }
            txtR7C4.Text = _setup.puzzleSolution._puzzle[7, 4].ToString();


            if (_setup.setupPuzzle._puzzle[7, 5] == 0)
            {
                txtR7C5.BackColor = Color.LightBlue;
            }
            txtR7C5.Text = _setup.puzzleSolution._puzzle[7, 5].ToString();

            if (_setup.setupPuzzle._puzzle[7, 6] == 0)
            {
                txtR7C6.BackColor = Color.LightBlue;
            }
            txtR7C6.Text = _setup.puzzleSolution._puzzle[7, 6].ToString();


            if (_setup.setupPuzzle._puzzle[7, 7] == 0)
            {
                txtR7C7.BackColor = Color.LightBlue;
            }
            txtR7C7.Text = _setup.puzzleSolution._puzzle[7, 7].ToString();

            if (_setup.setupPuzzle._puzzle[7, 8] == 0)
            {
                txtR7C8.BackColor = Color.LightBlue;
            }
            txtR7C8.Text = _setup.puzzleSolution._puzzle[7, 8].ToString();

        }

        private void fillRowNine()
        {
            if (_setup.setupPuzzle._puzzle[8, 0] == 0)
            {
                txtR8C0.BackColor = Color.LightBlue;
            }
            txtR8C0.Text = _setup.puzzleSolution._puzzle[8, 0].ToString();

            if (_setup.setupPuzzle._puzzle[8, 1] == 0)
            {
                txtR8C1.BackColor = Color.LightBlue;
            }
            txtR8C1.Text = _setup.puzzleSolution._puzzle[8, 1].ToString();


            if (_setup.setupPuzzle._puzzle[8, 2] == 0)
            {
                txtR8C2.BackColor = Color.LightBlue;
            }
            txtR8C2.Text = _setup.puzzleSolution._puzzle[8, 2].ToString();


            if (_setup.setupPuzzle._puzzle[8, 3] == 0)
            {
                txtR8C3.BackColor = Color.LightBlue;
            }
            txtR8C3.Text = _setup.puzzleSolution._puzzle[8, 3].ToString();

            if (_setup.setupPuzzle._puzzle[8, 4] == 0)
            {
                txtR8C4.BackColor = Color.LightBlue;
            }
            txtR8C4.Text = _setup.puzzleSolution._puzzle[8, 4].ToString();


            if (_setup.setupPuzzle._puzzle[8, 5] == 0)
            {
                txtR8C5.BackColor = Color.LightBlue;
            }
            txtR8C5.Text = _setup.puzzleSolution._puzzle[8, 5].ToString();

            if (_setup.setupPuzzle._puzzle[8, 6] == 0)
            {
                txtR8C6.BackColor = Color.LightBlue;
            }
            txtR8C6.Text = _setup.puzzleSolution._puzzle[8, 6].ToString();


            if (_setup.setupPuzzle._puzzle[8, 7] == 0)
            {
                txtR8C7.BackColor = Color.LightBlue;
            }
            txtR8C7.Text = _setup.puzzleSolution._puzzle[8, 7].ToString();

            if (_setup.setupPuzzle._puzzle[8, 8] == 0)
            {
                txtR8C8.BackColor = Color.LightBlue;
            }
            txtR8C8.Text = _setup.puzzleSolution._puzzle[8, 8].ToString();

        }





    }
}
