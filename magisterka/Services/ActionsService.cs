using System.Collections.Generic;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;
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

        public ActionsService(IFormData formData, IFileService fileService,
            ICoverageDataConverter coverageDataConverter, ICoverageFileValidator coverageFileValidator,
            IGranuleService granuleService, IGranuleSetDtoConverter granuleSetDtoConverter)
        {
            _formData = formData;
            _fileService = fileService;
            _coverageDataConverter = coverageDataConverter;
            _coverageFileValidator = coverageFileValidator;
            _granuleService = granuleService;
            _granuleSetDtoConverter = granuleSetDtoConverter;
        }

        public bool Load()
        {
            var path = _fileService.GetPathFromOpenFileDialog(FileService.CsvFilter);
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var content = _fileService.ReadFile(path);
            if (content == null)
            {
                return false;
            }

            var data = _coverageDataConverter.Convert(content);
            if (data == null)
            {
                return false;
            }

            var coverageFile = new CoverageFile(path, data);
            if (!_coverageFileValidator.ValidAndShow(coverageFile))
            {
                return false;
            }

            var zbGran = _granuleService.GenerateGran(coverageFile.CoverageData);
            _formData.PathSource = path;
            _formData.GranuleSet = zbGran;

            return true;
        }

        //TODO: add tests, add messages, try catch
        public bool SerializeGranuleSetAndSaveFile()
        {
            var granuleSet = _formData.GranuleSet;
            if (granuleSet == null)
            {
                return false;
            }

            var path = _fileService.GetPathFromSaveFileDialog(FileService.JsonFilter);
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var granulesDto = _granuleSetDtoConverter.ConvertToDto(granuleSet);
            var json = JsonConvert.SerializeObject(granulesDto);
            return _fileService.SaveFile(path, new List<string> {json});
        }

        //TODO: add tests, add messages, try catch
        public bool OpenFileAndDeserializeGranuleSet()
        {
            var path = _fileService.GetPathFromOpenFileDialog(FileService.JsonFilter);
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            var content = _fileService.ReadFile(path);
            if (content.Count != 1)
            {
                //message, error ?
                return false;
            }

            var json = content[0];
            var granulesDto = JsonConvert.DeserializeObject<GranuleDto[]>(json);
            var granuleSet = _granuleSetDtoConverter.ConvertFromDto(granulesDto);

            _formData.PathSource = path;
            _formData.GranuleSet = granuleSet;
            return true;
        }
    }
}
