using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BaldrickGUI {
    internal static class Utilities {
        internal static Tuple<int,int>? ParseTLC(string tlc) {
            Tuple<int, int> result = null;

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
    }
}
