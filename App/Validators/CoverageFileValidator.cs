using System.Linq;
using magisterka.Models;

namespace magisterka.Validators
{
    public class CoverageFileValidator : ICoverageFileValidator
    {
        public bool Valid(CoverageFile coverageFile, out string error)
        {
            error = null;

            if (coverageFile == null)
            {
                error = "Obiekt jest pusty.";
            }
            else if (string.IsNullOrEmpty(coverageFile.Path))
            {
                error = "Ścieżka do pliku jest pusta.";
            }
            else if (coverageFile.CoverageData == null || coverageFile.CoverageData.Count == 0)
            {
                error = "Wczytany plik nie zawiera poprawnych danych.";
            }
            else
            {
                var length = coverageFile.CoverageData[0].Count;
                if (coverageFile.CoverageData.Any(x => x.Count != length))
                {
                    error = "Wiersze muszą mieć jednakową ilość kolumn.";
                }
            }
            
            return error == null;
        }
    }
}
