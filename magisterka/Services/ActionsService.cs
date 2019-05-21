using System;
using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;
using magisterka.Wrappers;

namespace magisterka.Services
{
    public class ActionsService : IActionService
    {
        private readonly IFileService _fileService;
        private readonly ICoverageDataConverter _coverageDataConverter;
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly IGranuleService _granuleService;
        private readonly IGranuleSetDtoConverter _granuleSetDtoConverter;
        private readonly IMyJsonConvert _myJsonConvert;
        private readonly IPrintGranuleService _printGranuleService;

        public ActionsService(IFileService fileService, IPrintGranuleService printGranuleService,
            ICoverageDataConverter coverageDataConverter, ICoverageFileValidator coverageFileValidator,
            IGranuleService granuleService, IGranuleSetDtoConverter granuleSetDtoConverter,
            IMyJsonConvert jsonConvert)
        {
            _fileService = fileService;
            _coverageDataConverter = coverageDataConverter;
            _coverageFileValidator = coverageFileValidator;
            _granuleService = granuleService;
            _granuleSetDtoConverter = granuleSetDtoConverter;
            _myJsonConvert = jsonConvert;
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

        public bool SerializeGranuleSetAndSaveFile(GranuleSet granuleSet, out string error)
        {
            error = null;
            if (granuleSet == null)
            {
                error = "Pusty zbiór granul.";
                return false;
            }

            var path = _fileService.GetPathFromSaveFileDialog(FileService.JsonFilter);
            if (string.IsNullOrEmpty(path))
            {
                if (path == string.Empty)
                {
                    error = "Ścieżka do pliku jest pusta.";
                }

                return false;
            }

            var granulesDto = _granuleSetDtoConverter.ConvertToDto(granuleSet);
            try
            {
                var json = _myJsonConvert.SerializeObject(granulesDto);
                return _fileService.SaveFile(path, new List<string> { json }, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public GranuleSetWithPath OpenFileAndDeserializeGranuleSet(out string error)
        {
            error = null;
            var path = _fileService.GetPathFromOpenFileDialog(FileService.JsonFilter);
            if (string.IsNullOrEmpty(path))
            {
                if (path == string.Empty)
                {
                    error = "Ścieżka do pliku jest pusta.";
                }

                return null;
            }

            var content = _fileService.ReadFile(path, out  error);
            if (content == null)
            {
                return null;
            }
            if (content.Count != 1)
            {
                error = "Nieprawidłowa zawartość pliku json.";
                return null;
            }

            try
            {
                var json = content[0];
                var granulesDto = _myJsonConvert.DeserializeObject<GranuleDto[]>(json);
                var granuleSet = _granuleSetDtoConverter.ConvertFromDto(granulesDto);
                return new GranuleSetWithPath(granuleSet, path);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }
    }
}
