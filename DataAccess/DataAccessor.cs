using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class DataAccessor
    {
        private string appPath = AppContext.BaseDirectory;
        const string dataFolder = "saveData\\";
        const string solutionAndSetupFile = "sudokuFile.txt";
        const string pencilMarksFile = "pencilMarks.txt";
        private int _puzzleSize = 9;

        public DataAccessor()
        {
            string path = appPath + dataFolder;

            try
            {
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public void SaveSolutionAndSetup(List<int[,]> puzzlesToSave)
        {
            // save data needs a solution, a setup
            try
            {
                StreamWriter fileWriter = new StreamWriter(appPath + dataFolder + solutionAndSetupFile);                

                foreach (int[,] puzzle in puzzlesToSave)
                {
                    for (int row = 0; row < _puzzleSize; row++)
                    {
                        for (int col = 0; col < _puzzleSize; col++)
                        {

                            if (puzzle[row,col].Equals(""))
                            {
                                continue;
                            }
                            fileWriter.Write(puzzle[row, col] + ",");
                        }
                    }
                    fileWriter.WriteLine();
                }
                fileWriter.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SavePencilMarks (List<string[]> brokenUpPencilMarks)
        {
            
            try
            {
                StreamWriter fileWriter = new StreamWriter(appPath + dataFolder + pencilMarksFile);

                foreach (string[] pencilMark in brokenUpPencilMarks)
                {
                    fileWriter.WriteLine(pencilMark[0] + "," + pencilMark[1]);
                }
                fileWriter.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<string[]> RetrievePencilMarks()
        {
            List<string[]> pencilMarkRaw = new List<string[]>();

            char[] seperators = { ',' };

            try
            {
                StreamReader fileReader = new StreamReader(appPath + dataFolder + pencilMarksFile);

                while (fileReader.EndOfStream == false)
                {
                    string line = fileReader.ReadLine();

                    if (line.Length >= 4)
                    {
                        string[] parts = line.Split(seperators);
                        pencilMarkRaw.Add(parts);                    
                    }
                }

                fileReader.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return pencilMarkRaw;
        }

        public List<int[,]> RetrieveSaveSolAndSetup()
        {
            List<int[,]> puzzlesToLoad = new List<int[,]>();

            char[] seperators = { ','};

            try
            {
                StreamReader fileReader = new StreamReader(appPath + dataFolder + solutionAndSetupFile);

                while (fileReader.EndOfStream == false)
                {
                    string line = fileReader.ReadLine();

                    if (line.Length >= 7)
                    {
                        string[] parts = line.Split(seperators);
                        int partsIndex = 0;

                        if (parts.Count() == 82)
                        {
                            int[,] puzzle = new int[_puzzleSize, _puzzleSize];

                            for (int row = 0; row < _puzzleSize; row++)
                            {
                                for (int col = 0; col < _puzzleSize; col++)
                                {
                                    puzzle[row, col] = Int32.Parse(parts[partsIndex]);
                                    partsIndex++;
                                }
                            }
                            puzzlesToLoad.Add(puzzle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return puzzlesToLoad;
        }
    }
}
