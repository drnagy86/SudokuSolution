using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Suduko
{
    public static class UIHelpers
    {
        public static void CloseOpenWindows()
        {
            //Got help for closing frmPuzzleSolution from here on April 15, 2021:
            // https://stackoverflow.com/questions/9029351/close-all-open-forms-except-the-main-menu-in-c-sharp/9029389

            // close solution window if up
            List<Form> open = new List<Form>();

            foreach (Form openWin in Application.OpenForms)
            {
                open.Add(openWin);
            }

            foreach (Form item in open)
            {
                if (item.Name == "frmPuzzleSolution")
                {
                    item.Close();
                }
                if (item.Name == "frmFeatures")
                {
                    item.Close();
                }
            }
        }

        public static void ClearControls(Control.ControlCollection controlCollection)
        {
            // This method came from on 4/15/2021:
            // https://www.codeproject.com/Questions/813344/Clear-all-textboxes-in-form-with-one-Function

            foreach (Control control in controlCollection)
            {
                if (control is TextBox)
                {
                    //reset these properties
                    control.Text = "";
                    control.BackColor = SystemColors.Window;
                    control.Enabled = true;
                    // refresh turns all the boxes white while getting a new puzzle
                    control.Refresh();
                }
                else
                {
                    ClearControls(control.Controls);
                }
            }            
        }

        public static void UserAnswersFormat(Control ctrl)
        {
            ctrl.BackColor = Color.LightBlue;
            ctrl.Font = new Font(ctrl.Font.FontFamily, 26);
        }

        public static void PencilMarkFormat(Control ctrl)
        {            
            ctrl.BackColor = Color.LightYellow;
            ctrl.Font = new Font(ctrl.Font.FontFamily, 8);
        }

        public static int[] GetTxtBoxCoords(Control ctrl)
        {
            string rawTagString = ctrl.Tag.ToString();
            char[] seperator = { ',' };
            string[] tagStringArray = rawTagString.Split(seperator);

            int[] txtBoxCoords = { Int32.Parse(tagStringArray[0]), Int32.Parse(tagStringArray[1]) };

            return txtBoxCoords;
        }

        public static void DoubleClickColorChange(object sender)
        {
            //toggle the colors
            var control = (Control)sender;            

            if (control.BackColor == Color.LightBlue || control.BackColor == Color.LightYellow)
            {
                // not happy
                control.BackColor = Color.MistyRose;
            }
            else if (control.BackColor == Color.MistyRose)
            {
                // happy
                control.BackColor = Color.LightGreen;
            }
            else if (control.BackColor == Color.LightGreen)
            {
                if (control.Text.Length == 1 || control.Text.Equals(""))
                {
                    control.BackColor = Color.LightBlue;                    
                }
                else
                {
                    control.BackColor = Color.LightYellow;
                }                
            }
        }
    }

    enum Direction
    {
        right,
        left,
        above,
        below
    }
}
