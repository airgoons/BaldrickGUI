using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaldrickGUI {
    internal static class Utilities {
        private const int Baldrick_DataColumns = 9;

        internal static Tuple<int,int>? ParseTLC(string tlc) {
            Tuple<int, int>? result = null;

            // begin direct copy from google AI result (lol, lmao)
            if (string.IsNullOrEmpty(tlc)) {
                throw new ArgumentException("Cell reference cannot be null or empty.");
            }

            string columnLetters = "";
            string rowNumbers = "";

            foreach (char c in tlc) {
                if (char.IsLetter(c)) {
                    columnLetters += c;
                }
                else if (char.IsDigit(c)) {
                    rowNumbers += c;
                }
                else {
                    throw new FormatException("Invalid character in cell reference: " + c);
                }
            }

            if (string.IsNullOrEmpty(columnLetters) || string.IsNullOrEmpty(rowNumbers)) {
                throw new FormatException("Invalid cell reference format.");
            }

            int column = 0;
            foreach (char c in columnLetters.ToUpper()) {
                column = column * 26 + (c - 'A' + 1);
            }

            int row = int.Parse(rowNumbers);
            // end direct copy from google AI result

            result = Tuple.Create(column, row);

            return result;
        }

        internal static List<string> ExtractDataFromCSV(string path, Tuple<int, int>? tlc)
        {
            List<string> outputRows = new List<string>();

            using (var reader = new StreamReader(path))
            {
                var raw_data = reader.ReadToEnd();
                raw_data = raw_data.Replace("\r\n", "\n");  // remove CRLF nonsense

                if (tlc != null) {

                    var rows = raw_data.Split("\n").Skip(tlc.Item2 - 1);

                    foreach (var row in rows) {
                        var raw_row = row.Split(",").Skip(tlc.Item1).ToList().GetRange(0, Baldrick_DataColumns);
                        var data = String.Join(",", raw_row).Replace("\"", "");
                        if (data.Contains("#VALUE!")) {
                            break;  // End of useful data,   Note:  Tailored to Castor's GSheet
                        }
                        outputRows.Add(data);
                    }
                }
            }

            return outputRows;
        }
    }
}
