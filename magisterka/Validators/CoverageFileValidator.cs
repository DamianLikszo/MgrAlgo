﻿using System.Linq;
using magisterka.Interfaces;
using magisterka.Models;

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
            else if (coverageFile.Data.Count == 0)
            {
                errorMessage = "Wczytany plik nie zawiera poprawnych danych.";
            }
            else
            {
                var length = coverageFile.Data[0].Count;
                if (coverageFile.Data.Any(x => x.Count != length))
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
