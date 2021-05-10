using DataAccess;
using PuzzleEntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlLogic
{
    public class SaveLoad
    {

        private SetupPuzzle _setupPuzzle;
        private PuzzleNine _userAnswers;

        public SaveLoad()
        {
            _setupPuzzle = new SetupPuzzle();
            _userAnswers = new PuzzleNine(1);
        }

        //public SetupPuzzle SetupPuzzle
        //{
        //    get { return _setupPuzzle; }
        //    set { _setupPuzzle = value; }
        //}

        //public PuzzleNine UserAnswers
        //{
        //    get { return _userAnswers; }
        //    set { _userAnswers = value; }
        //}

        public bool SaveSolutionAndSetup(List<int[,]> solutionAndSetup)
        {
            bool result = false;
            try
            {
                DataAccessor dataAccessor = new DataAccessor();
                dataAccessor.SaveSolutionAndSetup(solutionAndSetup);
                result = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Your save location could not be created", ex);
            }
            return result;
        }

        //public List<int[,]> GetSetupPuzzle()
        //{
        //    List<int[,]> puzzlesToLoad = null;

        //    try
        //    {
        //        DataAccessor dataAccessor = new DataAccessor();
        //        puzzlesToLoad = dataAccessor.RetrieveSaveSolAndSetup();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new ApplicationException("Data not availiable.", ex);
        //    }
        //    return puzzlesToLoad;

        //}



    }
}
