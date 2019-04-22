using System.Linq;
using magisterka.Models;
using magisterka.Wrappers;

namespace magisterka.Validators
{
    public class CoverageFileValidator : ICoverageFileValidator
    {
        public bool Valid(CoverageFile coverageFile, out string errorMessage)
        {
            errorMessage = null;

            if (coverageFile == null)
            {
                errorMessage = "Obiekt jest pusty.";
            }
            else if (string.IsNullOrEmpty(coverageFile.Path))
            {
                errorMessage = "Ścieżka do pliku jest pusta.";
            }
            else if (coverageFile.CoverageData == null || coverageFile.CoverageData.Count == 0)
            {
                errorMessage = "Wczytany plik nie zawiera poprawnych danych.";
            }
            else
            {
                var length = coverageFile.CoverageData[0].Count;
                if (coverageFile.CoverageData.Any(x => x.Count != length))
                {
                    errorMessage = "Wiersze muszą mieć jednakową ilość liczb.";
                }
            }
            
            return string.IsNullOrEmpty(errorMessage);
        }

        public bool ValidAndShow(CoverageFile coverageFile, IMyMessageBox myMessageBox)
        {
            var result = Valid(coverageFile, out string errorMessage);

            if (!result)
            {
                myMessageBox.Show(errorMessage);
            }

            return result;
        }
    }
}
