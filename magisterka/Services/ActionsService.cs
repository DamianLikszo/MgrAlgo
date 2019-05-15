using System;
using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;
using magisterka.Wrappers;
using Newtonsoft.Json;

namespace magisterka.Services
{
    public class ActionsService : IActionService
    {
        private readonly IFormData _formData;
        private readonly IFileService _fileService;
        private readonly ICoverageDataConverter _coverageDataConverter;
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly IGranuleService _granuleService;
        private readonly IGranuleSetDtoConverter _granuleSetDtoConverter;
        private readonly IMyJsonConvert _myJsonConvert;

        public ActionsService(IFormData formData, IFileService fileService,
            ICoverageDataConverter coverageDataConverter, ICoverageFileValidator coverageFileValidator,
            IGranuleService granuleService, IGranuleSetDtoConverter granuleSetDtoConverter,
            IMyJsonConvert jsonConvert)
        {
            _formData = formData;
            _fileService = fileService;
            _coverageDataConverter = coverageDataConverter;
            _coverageFileValidator = coverageFileValidator;
            _granuleService = granuleService;
            _granuleSetDtoConverter = granuleSetDtoConverter;
            _myJsonConvert = jsonConvert;
        }

        public bool Load(out string error)
        {
            error = null;
            var path = _fileService.GetPathFromOpenFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                error = "Empty path file.";
                return false;
            }

            var content = _fileService.ReadFile(path, out error);
            if (!string.IsNullOrEmpty(error))
            {
                return false;
            }

            var data = _coverageDataConverter.Convert(content, out error);
            if (data == null)
            {
                return false;
            }

            var coverageFile = new CoverageFile(path, data);
            if (!_coverageFileValidator.Valid(coverageFile, out error))
            {
                return false;
            }

            var zbGran = _granuleService.GenerateGran(coverageFile.CoverageData);
            _formData.PathSource = path;
            _formData.GranuleSet = zbGran;

            return true;
        }

        public bool SerializeGranuleSetAndSaveFile(out string error)
        {
            error = null;
            var granuleSet = _formData.GranuleSet;
            if (granuleSet == null)
            {
                error = "Empty granule set object";
                return false;
            }

            var path = _fileService.GetPathFromSaveFileDialog(FileService.JsonFilter);
            if (string.IsNullOrEmpty(path))
            {
                error = "Empty file path.";
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

        public bool OpenFileAndDeserializeGranuleSet(out string error)
        {
            error = null;
            var path = _fileService.GetPathFromOpenFileDialog(FileService.JsonFilter);
            if (string.IsNullOrEmpty(path))
            {
                error = "Empty path file.";
                return false;
            }

            var content = _fileService.ReadFile(path, out  error);
            if (content == null)
            {
                return false;
            }
            if (content.Count != 1)
            {
                error = "Wrong json content.";
                return false;
            }

            try
            {
                var json = content[0];
                var granulesDto = _myJsonConvert.DeserializeObject<GranuleDto[]>(json);
                var granuleSet = _granuleSetDtoConverter.ConvertFromDto(granulesDto);

                _formData.PathSource = path;
                _formData.GranuleSet = granuleSet;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
