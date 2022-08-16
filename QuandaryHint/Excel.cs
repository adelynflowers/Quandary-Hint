/*
 * 
 * 
 * 
 * PLEASE READ:
 * This file is currently in the process of being excluded from the build and project. It is something I would like
 * to explore further, but I think it would be better suited for the Electron rewrite. I will leave the code here as
 * reference to how to write to the excel sheet formatting wise.
 * 
using System;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace QuandaryHint
{
    class Excel
    {
        #region Variables
        //Path to store the file location
        public string path;

        //The object being wrapped
        public _Application excel = new _Excel.Application();

        //Workbook to be used
        public  Workbook wb;

        //Worksheet to be used
        public  Worksheet ws;

        //Stores the row that we want to append to
        public int FirstEmptyRow;

        //Stores the column we want to append to
        public int gameColumn;
        #endregion

        #region Constructors/Destructors
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sheet"></param>
        /// <param name="column"></param>
        public Excel(string path, int sheet, int column)
        {
            //Set the path variables
            this.path = path;

            if (File.Exists(path))
            {
                //Open up the sheet we want
                wb = excel.Workbooks.Open(path);
                ws = wb.Worksheets[sheet];
            }

            else
            {
                Console.WriteLine("File does not exist. creating new workbook");
                wb = excel.Workbooks.Add("");
                Console.WriteLine("Selecting worksheet 0");
                ws = wb.ActiveSheet;
                Console.WriteLine("Time to fill it in and save it");
                CreateEscapeSpreadsheet();
            }


            //Find which part of the sheet we'll be appending to
            gameColumn = column;
            FindFirstEmptyRow(column);

            //Hide it all from the user
            excel.Visible = false;
        }

        /// <summary>
        /// Destructor that saves, quits, and releases the object
        /// </summary>
        ~Excel()
        {
            SaveAndQuit();
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Return the value of a particular cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public string ReadCell(int row, int col)
        {
            if (ws.Cells[row, col].Value2 != null)
                return ws.Cells[row, col].Value2;
            else
                return "";
        }

        /// <summary>
        /// Write a string to a cell
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="data"></param>
        public void WriteCell(int row, int col, string data)
        {
            ws.Cells[row, col] = data;
            Console.WriteLine("Wrote " + data + "\n");
           
           
        }

        /// <summary>
        /// Append game information to the Quandary spreadsheet
        /// </summary>
        /// <param name="teamSize"></param>
        /// <param name="teamName"></param>
        /// <param name="escapeTime"></param>
        /// <param name="escaped"></param>
        public void AppendToDocument(int teamSize, string teamName, string escapeTime, bool escaped)
        {
            //Get/write the date
            DateTime thisDay = DateTime.Today;
            WriteCell(FirstEmptyRow, gameColumn, thisDay.ToString("d"));
            
            //Write the game results
            WriteCell(FirstEmptyRow, gameColumn + 1, teamSize.ToString());
            WriteCell(FirstEmptyRow, gameColumn + 2, teamName);
            WriteCell(FirstEmptyRow, gameColumn + 3, escapeTime);

            //Did they escape?
            string txt = (escaped) ? "Yes" : "No";
            WriteCell(FirstEmptyRow, gameColumn + 4, txt);

            //Get to the next empty row without a method call
            FirstEmptyRow++;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Saves quits, and releases the objects
        /// </summary>
        private void SaveAndQuit()
        {
            wb.Save();
            excel.Workbooks.Close();
            excel.Quit();
            Marshal.ReleaseComObject(ws);
            Marshal.ReleaseComObject(wb);
        }

        /// <summary>
        /// Find the first empty row in a column
        /// </summary>
        /// <param name="column"></param>
        private void FindFirstEmptyRow(int column)
        {
            //Excel arrays start at 1 :(
            int row = 1;

            //Find the beginning of the listings
            while (ws.Cells[row, column].Value2 != "Date")
            {
                row++;
            }

            //keeep going until you find a blank row
            while (ws.Cells[row, column].Value2 != null)
                row++;

            FirstEmptyRow = row;
        }

        /// <summary>
        /// Creates a new valid spreadsheet to be used with the program
        /// </summary>
        private void CreateEscapeSpreadsheet()
        {
            //Write in a set of headers for each game mode..
            //excel sheets start at 1 :(
            path = @"c:\QuandarySpreadsheet.xlsx";

            //If there's already a placeholder file, delete it to shield
            //the user from a overwriting file message
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            for (int i = 2; i <= 14; i += 6)
            {
                WriteCell(1, i, "Date");
                WriteCell(1, i + 1, "Size");
                WriteCell(1, i + 2, "TeamSize");
                WriteCell(1, i + 3, "EscapeT");
                WriteCell(1, i + 4, "Escaped?");
            }
 
            Console.WriteLine("Creating file...");
            wb.SaveAs(path);
        }
        #endregion
    }
}
*/