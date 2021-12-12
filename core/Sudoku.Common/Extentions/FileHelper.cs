using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku.Common
{
    public static class FileHelper
    {
        private static List<char> PossibleValues = new List<char> { '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
       
        public static int[,] ReadAsList(this IFormFile file, int size)
        {
            var result = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var row = reader.ReadLine();
                    if (String.IsNullOrEmpty(row)) continue;

                    row.IsRowContainsWrongCharacter(size);
                    row.IsColumnSizeIsCorrect(size);

                    result.Add(row.Trim().Replace('.','0'));
                } 
            }

            IsColumnSizeIsCorrect(result.Count, size);
            var matrix = ConvertToMatrix(result, size);
            return matrix;
        }

        private static int[,] ConvertToMatrix(List<string> textMatrix, int size)
        {
            var result = new int[size, size];
            int rowNumber = 0;
            foreach (var row in textMatrix)
            {
                int columnNumber = 0;
                foreach (var column in row.Trim())
                {
                    result[rowNumber, columnNumber++] = (int)Char.GetNumericValue(column);
                }
                rowNumber++;
            }
            return result;
        }

        private static void IsRowContainsWrongCharacter(this string row, int size)
        {
            var fileWrongContent = row.Trim().ToArray().Except(PossibleValues);
            if (fileWrongContent.Count() > 0)
                throw new Exception("The file which you has uploaded contains wrong characters. Please remove charecters without those:" + String.Join(',', PossibleValues));
        }

        private static void IsColumnSizeIsCorrect(this string row, int size)
        {
            if (row.Trim().Length != size)
                throw new Exception("The file which you has uploaded contains wrong column size. Every column should have "+size+" characters.");
        }

        private static void IsColumnSizeIsCorrect(int fileRowCount, int size)
        {
            if (fileRowCount != size)
                throw new Exception("The file which you has uploaded contains wrong row size. Every row should have " + size + " characters.");
        }
    }
}
