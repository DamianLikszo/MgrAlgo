using System.Windows.Forms;
using App.Interfaces;
using App.Models;
using App.Validators;
using magisterka.Interfaces;

namespace App.Services
{
    public class ActionsService : IActionService
    {
        private readonly IFileService _fileService;
        private readonly ICoverageDataConverter _coverageDataConverter;
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly IGranuleService _granuleService;
        private readonly IPrintGranuleService _printGranuleService;
        private readonly IPrintGranSetService _printGranSetService;

        public ActionsService(IFileService fileService, IPrintGranuleService printGranuleService,
            ICoverageDataConverter coverageDataConverter, ICoverageFileValidator coverageFileValidator,
            IGranuleService granuleService, IPrintGranSetService printGranSetService)
        {
            _fileService = fileService;
            _coverageDataConverter = coverageDataConverter;
            _coverageFileValidator = coverageFileValidator;
            _granuleService = granuleService;
            _printGranSetService = printGranSetService;
            _printGranuleService = printGranuleService;
        }

        public GranuleSetWithPath Load(out string error)
        {
            error = null;
            var path = _fileService.GetPathFromOpenFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                if (path == string.Empty)
                {
                    error = "Ścieżka do pliku jest pusta.";
                }

                return null;
            }

            var content = _fileService.ReadFile(path, out error);
            if (!string.IsNullOrEmpty(error))
            {
                return null;
            }

            var data = _coverageDataConverter.Convert(content, out error);
            if (data == null)
            {
                return null;
            }

            var coverageFile = new CoverageFile(path, data);
            if (!_coverageFileValidator.Valid(coverageFile, out error))
            {
                return null;
            }

            var granuleSet = _granuleService.GenerateGran(coverageFile.CoverageData);
            return new GranuleSetWithPath(granuleSet, path);
        }

        public bool SaveGranule(GranuleSet granuleSet, out string error)
        {
            error = null;
            if (granuleSet == null)
            {
                error = "Pusty zbiór granul.";
                return false;
            }

            var path = _fileService.GetPathFromSaveFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                if (path == string.Empty)
                {
                    error = "Ścieżka do pliku jest pusta.";
                }

                return false;
            }

            var content = _printGranuleService.Print(granuleSet);
            return _fileService.SaveFile(path, content, out error);
        }

        public bool SaveMaxChains(TreeNode[] chains, out string error)
        {
            error = null;
            if (chains == null || chains.Length == 0)
            {
                error = "Brak łańcuchów maksymalnych.";
                return false;
            }

            var path = _fileService.GetPathFromSaveFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                if (path == string.Empty)
                {
                    error = "Ścieżka do pliku jest pusta.";
                }

                return false;
            }

            var content = _printGranSetService.Print(chains);
            return _fileService.SaveFile(path, content, out error);
        }
    }
}
